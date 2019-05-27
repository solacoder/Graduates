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
    public class CandidateService : ICandidateService
    {
        private readonly IUnitOfWork _uow;

        public CandidateService(IUnitOfWork uow)
        {
            this._uow = uow;
        }
        public IEnumerable<Candidate> GetAll()
        {
            return _uow.Candidates.GetAll();
        }

        public Candidate GetById(long Id)
        {
            return _uow.Candidates.Get(Id);
        }

        public bool Remove(Candidate obj)
        {
            bool state = false;

            _uow.Candidates.Remove(obj);
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

            var obj = _uow.Candidates.Get(id);

            _uow.Candidates.Remove(obj);
            int result = _uow.Complete();
            if (result > 0)
            {
                state = true;
            }
            return state;
        }

        public bool Save(Candidate obj, ref string message)
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

        private bool Add(Candidate obj, ref string message)
        {
            bool state = false;

            // Check if there is an existing name
            if (!_uow.Candidates.IsExists(obj))
            {
                _uow.Candidates.Add(obj);
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

        private bool Update(long Id, Candidate obj)
        {
            bool state = false;

            var objEx = _uow.Candidates.Get(Id);
            objEx = obj;
            objEx.Id = (int)Id;
            _uow.Candidates.Update(Id, objEx);
            int result = _uow.Complete();
            if (result > 0)
            {
                state = true;
            }
            return state;
        }

        public DataTablesResponse SearchApi(IDataTablesRequest requestModel)
        {
            IQueryable<Candidate> query = _uow.Candidates.GetAll().AsQueryable();

            var totalCount = query.Count();

            // Apply filters
            if (!string.IsNullOrEmpty(requestModel.Search.Value))
            {
                var value = requestModel.Search.Value.Trim();
                query = query.Where(p => p.FirstName.Contains(value) 
                                    || p.LastName.Contains(value) || p.Email.Contains(value));
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
                                      CandidateNo = tr.CandidateNo,
                                      DateOfBirth = tr.DateOfBirth.Date.ToString("dd/MM/yyyy"),
                                      Name = tr.FirstName + " " + tr.LastName,
                                      tr.Email,
                                      tr.Phone,
                                      Sex = tr.Sex.Name
                                  };

            DataTablesResponse response = DataTablesResponse.Create(requestModel, totalCount, filteredCount, transformedData);
            return response;
        }
    }
}
