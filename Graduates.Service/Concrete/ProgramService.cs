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
    public class ProgramService : IProgramService
    {
        private readonly IUnitOfWork _uow;

        public ProgramService(IUnitOfWork uow)
        {
            this._uow = uow;
        }
        public IEnumerable<Program> GetAll()
        {
            return _uow.Programs.GetAll();
        }

        public Program GetById(long Id)
        {
            return _uow.Programs.Get(Id);
        }

        public bool Remove(Program obj)
        {
            bool state = false;

            _uow.Programs.Remove(obj);
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

            var obj = _uow.Programs.Get(id);

            _uow.Programs.Remove(obj);
            int result = _uow.Complete();
            if (result > 0)
            {
                state = true;
            }
            return state;
        }

        public bool Save(Program obj, ref string message)
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

        private bool Add(Program obj, ref string message)
        {
            bool state = false;

            // Check if there is an existing name
            if (!_uow.Programs.IsExists(obj))
            {
                _uow.Programs.Add(obj);
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

        private bool Update(long Id, Program obj)
        {
            bool state = false;

            var objEx = _uow.Programs.Get(Id);
            objEx = obj;
            objEx.Id = (int)Id;
            _uow.Programs.Update(Id, objEx);
            int result = _uow.Complete();
            if (result > 0)
            {
                state = true;
            }
            return state;
        }

        public DataTablesResponse SearchApi(IDataTablesRequest requestModel)
        {
            IQueryable<Program> query = _uow.Programs.GetAll().AsQueryable();

            var totalCount = query.Count();

            // Apply filters
            if (!string.IsNullOrEmpty(requestModel.Search.Value))
            {
                var value = requestModel.Search.Value.Trim();
                query = query.Where(p => p.ProgramDurationUnit.Name.Contains(value));
            }

            var filteredCount = query.Count();

            // Sorting
            var orderColums = requestModel.Columns.Where(x => x.Sort != null);

            //paging
            var data = query.OrderBy(orderColums).Skip(requestModel.Start).Take(requestModel.Length);

            var transformedData = from tr in data
                                  select new
                                  {
                                      Id = tr.Id
                                      
                                  };
            var myList = transformedData.ToArray();

            DataTablesResponse response = DataTablesResponse.Create(requestModel, totalCount, filteredCount, transformedData);
            return response;
        }

        public IEnumerable<Program> GetProgramsByCourseId(long courseId)
        {
            return _uow.Programs.GetAll().Where(m => m.CourseId == courseId);
        }
    }
}
