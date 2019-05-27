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
    public class CourseService : ICourseService
    {
        private readonly IUnitOfWork _uow;

        public CourseService(IUnitOfWork uow)
        {
            this._uow = uow;
        }
        public IEnumerable<Course> GetAll()
        {
            return _uow.Courses.GetAll();
        }

        public Course GetById(long Id)
        {
            return _uow.Courses.Get(Id);
        }

        public bool Remove(Course obj)
        {
            bool state = false;

            _uow.Courses.Remove(obj);
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

            var obj = _uow.Courses.Get(id);

            _uow.Courses.Remove(obj);
            int result = _uow.Complete();
            if (result > 0)
            {
                state = true;
            }
            return state;
        }

        public bool Save(Course obj, ref string message)
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

        private bool Add(Course obj, ref string message)
        {
            bool state = false;

            // Check if there is an existing name
            if (!_uow.Courses.IsExists(obj))
            {
                _uow.Courses.Add(obj);
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

        private bool Update(long Id, Course obj)
        {
            bool state = false;

            var objEx = _uow.Courses.Get(Id);
            objEx = obj;
            objEx.Id = (int)Id;
            _uow.Courses.Update(Id, objEx);
            int result = _uow.Complete();
            if (result > 0)
            {
                state = true;
            }
            return state;
        }

        public DataTablesResponse SearchApi(IDataTablesRequest requestModel)
        {
            IQueryable<Course> query = _uow.Courses.GetAll().AsQueryable();

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
                                      Email = tr.Email,
                                      Phone = tr.Phone,
                                      Institution = tr.Institution.Name,
                                      Faculty = tr.Faculty.Name,
                                      Department = tr.Department.Name,
                                  };
            var myList = transformedData.ToArray();

            DataTablesResponse response = DataTablesResponse.Create(requestModel, totalCount, filteredCount, transformedData);
            return response;
        }

        public IEnumerable<Course> GetCoursesByDepartmentId(long departmentId)
        {
            return _uow.Courses.GetAll().Where(m => m.DepartmentId == departmentId);
        }
    }
}
