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
    public class InstitutionService : IInstitutionService
    {
        private readonly IUnitOfWork _uow;

        public InstitutionService(IUnitOfWork uow)
        {
            this._uow = uow;
        }
        public IEnumerable<Institution> GetAll()
        {
            return _uow.Institutions.GetAll();
        }

        public Institution GetById(long Id)
        {
            return _uow.Institutions.Get(Id);
        }

        public bool Remove(Institution obj)
        {
            bool state = false;

            _uow.Institutions.Remove(obj);
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

            var obj = _uow.Institutions.Get(id);

            _uow.Institutions.Remove(obj);
            int result = _uow.Complete();
            if (result > 0)
            {
                state = true;
            }
            return state;
        }

        public bool Save(Institution obj, ref string message)
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

        private bool Add(Institution obj, ref string message)
        {
            bool state = false;

            // Check if there is an existing name
            if (!_uow.Institutions.IsExists(obj))
            {
                _uow.Institutions.Add(obj);
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

        private bool Update(long Id, Institution obj)
        {
            bool state = false;

            var objEx = _uow.Institutions.Get(Id);
            objEx = obj;
            objEx.Id = (int)Id;
            _uow.Institutions.Update(Id, objEx);
            int result = _uow.Complete();
            if (result > 0)
            {
                state = true;
            }
            return state;
        }

        public DataTablesResponse SearchApi(IDataTablesRequest requestModel)
        {
            IQueryable<Institution> query = _uow.Institutions.GetAll().AsQueryable();

            var totalCount = query.Count();

            // Apply filters
            if (!string.IsNullOrEmpty(requestModel.Search.Value))
            {
                var value = requestModel.Search.Value.Trim();
                query = query.Where(p => p.Name.Contains(value));
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
                                      Name = tr.Name,
                                      Phone = tr.Phone,
                                      Email = tr.Email,
                                      YearEstablished = tr.YearEstablished,
                                      OwnershipType = tr.OwnershipType.Name,
                                      County = tr.County.Name,
                                      Type = tr.Type.Name,
                                      Website = tr.WebSite
                                  };
            var listValues = transformedData.ToList();

            DataTablesResponse response = DataTablesResponse.Create(requestModel, totalCount, filteredCount, transformedData);
            return response;
        }
    }
}
