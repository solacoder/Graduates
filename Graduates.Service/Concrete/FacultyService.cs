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
    public class FacultyService : IFacultyService
    {
        private readonly IUnitOfWork _uow;

        public FacultyService(IUnitOfWork uow)
        {
            this._uow = uow;
        }
        public IEnumerable<Faculty> GetAll()
        {
            return _uow.Faculties.GetAll();
        }

        public Faculty GetById(long Id)
        {
            return _uow.Faculties.Get(Id);
        }

        public bool Remove(Faculty obj)
        {
            bool state = false;

            _uow.Faculties.Remove(obj);
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

            var obj = _uow.Faculties.Get(id);

            _uow.Faculties.Remove(obj);
            int result = _uow.Complete();
            if (result > 0)
            {
                state = true;
            }
            return state;
        }

        public bool Save(Faculty obj, ref string message)
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

        private bool Add(Faculty obj, ref string message)
        {
            bool state = false;

            // Check if there is an existing name
            if (!_uow.Faculties.IsExists(obj))
            {
                _uow.Faculties.Add(obj);
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

        private bool Update(long Id, Faculty obj)
        {
            bool state = false;

            var objEx = _uow.Faculties.Get(Id);
            objEx = obj;
            objEx.Id = (int)Id;
            _uow.Faculties.Update(Id, objEx);
            int result = _uow.Complete();
            if (result > 0)
            {
                state = true;
            }
            return state;
        }

        public DataTablesResponse SearchApi(IDataTablesRequest requestModel)
        {
            IQueryable<Faculty> query = _uow.Faculties.GetAll().AsQueryable();

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
                                      Institution = tr.Institution.Name,
                                      Departments = tr.Departments.Count()
                                  };

            DataTablesResponse response = DataTablesResponse.Create(requestModel, totalCount, filteredCount, transformedData);
            return response;
        }

        public IEnumerable<Faculty> GetByInstitutionId(long InstitutionId)
        {
            return _uow.Faculties.GetAll().Where(m => m.InstitutionId == InstitutionId);
        }
    }
}
