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
    public class SetupNameService : ISetupNameService
    {
        private readonly IUnitOfWork _uow;

        public SetupNameService(IUnitOfWork uow)
        {
            this._uow = uow;
        }
        public IEnumerable<SetupName> GetAll()
        {
            return _uow.SetupNames.GetAll();
        }

        public SetupName GetById(long Id)
        {
            return _uow.SetupNames.Get(Id);
        }

        public bool Remove(SetupName obj)
        {
            bool state = false;

            _uow.SetupNames.Remove(obj);
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

            var obj = _uow.SetupNames.Get(id);

            _uow.SetupNames.Remove(obj);
            int result = _uow.Complete();
            if (result > 0)
            {
                state = true;
            }
            return state;
        }

        public bool Save(SetupName obj, ref string message)
        {
            if (obj.Id == 0)
            {
                obj.ParentId = obj.ParentId == 0 ? null : obj.ParentId;
                return Add(obj, ref message);
            }
            else
            {
                return Update(obj.Id, obj);
            }
        }

        private bool Add(SetupName obj, ref string message)
        {
            bool state = false;

            // Check if there is an existing name
            if (!_uow.SetupNames.IsExists(obj))
            {
                _uow.SetupNames.Add(obj);
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

        private bool Update(long Id, SetupName obj)
        {
            bool state = false;

            var objEx = _uow.SetupNames.Get(Id);
            objEx = obj;
            objEx.Id = (int)Id;
            _uow.SetupNames.Update(Id, objEx);
            int result = _uow.Complete();
            if (result > 0)
            {
                state = true;
            }
            return state;
        }

        public DataTablesResponse SearchApi(IDataTablesRequest requestModel)
        {
            IQueryable<SetupName> query = _uow.SetupNames.GetAll().AsQueryable();

            var totalCount = query.Count();

            // Apply filters
            if (!string.IsNullOrEmpty(requestModel.Search.Value))
            {
                var value = requestModel.Search.Value.Trim().ToLower();
                query = query.Where(p => p.Name.ToLower().Contains(value));
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
                                      ParentId = tr.ParentId,
                                      Parent = tr.Parent.Name
                                  };

            DataTablesResponse response = DataTablesResponse.Create(requestModel, totalCount, filteredCount, transformedData);
            return response;
        }
    }
}
