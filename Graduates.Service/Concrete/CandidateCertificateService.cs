using DataTables.AspNet.AspNetCore;
using DataTables.AspNet.Core;
using Graduates.Core;
using Graduates.Core.Entities;
using Graduates.Service.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Graduates.Service.Concrete
{
    public class CandidateCertificateService : ICandidateCertificateService
    {
        private readonly IUnitOfWork _uow;

        public CandidateCertificateService(IUnitOfWork uow)
        {
            this._uow = uow;
        }
        public IEnumerable<CandidateCertificate> GetAll()
        {
            return _uow.CandidateCertificates.GetAll();
        }

        public CandidateCertificate GetById(long Id)
        {
            return _uow.CandidateCertificates.Get(Id);
        }

        public bool Remove(CandidateCertificate obj)
        {
            bool state = false;

            _uow.CandidateCertificates.Remove(obj);
            int result = _uow.Complete();
            if (result > 0)
            {
                state = true;
            }
            return state;
        }

        public bool Remove(long id)
        {
            bool state = false;

            var obj = _uow.CandidateCertificates.Get(id);

            _uow.CandidateCertificates.Remove(obj);
            int result = _uow.Complete();
            if (result > 0)
            {
                state = true;
            }
            return state;
        }

        public bool Save(CandidateCertificate obj, ref string message)
        {
            if (obj.Id == 0)
            {
                return Add(obj, ref message);
            }
            else
            {
                return Update(obj.Id, obj);
            }
        }

        private bool Add(CandidateCertificate obj, ref string message)
        {
            bool state = false;

            // Check if there is an existing name
            if (!_uow.CandidateCertificates.IsExists(obj))
            {
                _uow.CandidateCertificates.Add(obj);
                int result = _uow.Complete();
                if (result > 0)
                {
                    state = true;
                }
            }
            else
            {
                message = "Data Exists!";
            }

            return state;
        }

        private bool Update(long Id, CandidateCertificate obj)
        {
            bool state = false;

            var objEx = _uow.CandidateCertificates.Get(Id);
            objEx = obj;
            objEx.Id = (int)Id;
            _uow.CandidateCertificates.Update(Id, objEx);
            int result = _uow.Complete();
            if (result > 0)
            {
                state = true;
            }
            return state;
        }

        public DataTablesResponse SearchApi(IDataTablesRequest requestModel)
        {
            IQueryable<CandidateCertificate> query = _uow.CandidateCertificates.GetAll().AsQueryable();

            var totalCount = query.Count();

            // Apply filters
            if (!string.IsNullOrEmpty(requestModel.Search.Value))
            {
                var value = requestModel.Search.Value.Trim();
                query = query.Where(p => p.CertificateNo.Contains(value));
            }

            var filteredCount = query.Count();

            // Sorting
            var orderColums = requestModel.Columns.Where(x => x.Sort != null);

            //paging
            var data = query.OrderBy(orderColums).Skip(requestModel.Start).Take(requestModel.Length);

            var transformedData = from tr in data
                                  select new
                                  {
                                      Id = tr.Id,
                                      CertificateNo = tr.CertificateNo,
                                      CertificateTitle = tr.CertificateTitle,
                                      Institution = tr.Institution.Name,
                                      Faculty = tr.Faculty.Name,
                                      Grade = tr.Grade.Name,
                                      StudentNo = tr.StudentNo,
                                      Course = tr.Course.Name,
                                      Department = tr.Department.Name,
                                      YearObtained = tr.YearObtained
                                  };

            DataTablesResponse response = DataTablesResponse.Create(requestModel, totalCount, filteredCount, transformedData);
            return response;
        }
    }
}
