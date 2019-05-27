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
    public class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork _uow;

        public DepartmentService(IUnitOfWork uow)
        {
            this._uow = uow;
        }
        public IEnumerable<Department> GetAll()
        {
            return _uow.Departments.GetAll();
        }

        public Department GetById(long Id)
        {
            return _uow.Departments.Get(Id);
        }

        public bool Remove(Department obj)
        {
            bool state = false;

            _uow.Departments.Remove(obj);
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

            var obj = _uow.Departments.Get(id);

            _uow.Departments.Remove(obj);
            int result = _uow.Complete();
            if (result > 0)
            {
                state = true;
            }
            return state;
        }

        public bool Save(Department obj, ref string message)
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

        private bool Add(Department obj, ref string message)
        {
            bool state = false;

            // Check if there is an existing name
            if (!_uow.Departments.IsExists(obj))
            {
                _uow.Departments.Add(obj);
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

        private bool Update(long Id, Department obj)
        {
            bool state = false;

            var objEx = _uow.Departments.Get(Id);
            objEx = obj;
            objEx.Id = (int)Id;
            _uow.Departments.Update(Id, objEx);
            int result = _uow.Complete();
            if (result > 0)
            {
                state = true;
            }
            return state;
        }

        public DataTablesResponse SearchApi(IDataTablesRequest requestModel)
        {
            IQueryable<Department> query = _uow.Departments.GetAll().AsQueryable();

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
                                      Email = tr.Email,
                                      Phone = tr.Phone,
                                      Name = tr.Name,
                                      Institution = tr.Institution.Name,
                                      Faculty = tr.Faculty.Name
                                  };

            DataTablesResponse response = DataTablesResponse.Create(requestModel, totalCount, filteredCount, transformedData);
            return response;
        }

        public IEnumerable<Department> GetDepartmentByFacultyId(long facultyId)
        {
            return _uow.Departments.GetAll().Where(m => m.FacultyId == facultyId);
        }
    }
}
