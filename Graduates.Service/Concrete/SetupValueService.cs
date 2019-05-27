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
    public class SetupValueService : ISetupValueService
    {
        private readonly IUnitOfWork _uow;

        public SetupValueService(IUnitOfWork uow)
        {
            this._uow = uow;
        }
        public IEnumerable<SetupValue> GetAll()
        {
            return _uow.SetupValues.GetAll();
        }

        public SetupValue GetById(long Id)
        {
            return _uow.SetupValues.Get(Id);
        }

        public bool Remove(SetupValue obj)
        {
            bool state = false;

            _uow.SetupValues.Remove(obj);
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

            var obj = _uow.SetupValues.Get(id);

            _uow.SetupValues.Remove(obj);
            int result = _uow.Complete();
            if (result > 0)
            {
                state = true;
            }
            return state;
        }

        public bool Save(SetupValue obj, ref string message)
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

        private bool Add(SetupValue obj, ref string message)
        {
            bool state = false;

            // Check if there is an existing name
            if (!_uow.SetupValues.IsExists(obj))
            {
                _uow.SetupValues.Add(obj);
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

        private bool Update(long Id, SetupValue obj)
        {
            bool state = false;

            var objEx = _uow.SetupValues.Get(Id);
            objEx = obj;
            objEx.Id = (int)Id;
            _uow.SetupValues.Update(Id, objEx);
            int result = _uow.Complete();
            if (result > 0)
            {
                state = true;
            }
            return state;
        }

        public DataTablesResponse SearchApi(IDataTablesRequest requestModel, long? SetUpNameId)
        {
            IQueryable<SetupValue> query = _uow.SetupValues.GetAll().AsQueryable();
            query = query.Where(m => m.SetupNameId == SetUpNameId);
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
                                      Parent = tr.Parent.Name,
                                      SetUpName = tr.SetupName.Name,
                                      
                                      ParentSetUpNameId = tr.SetupName.ParentId
                                  };

                

            DataTablesResponse response = DataTablesResponse.Create(requestModel, totalCount, filteredCount, transformedData);
            return response;
        }

        public DataTablesResponse SearchApi(IDataTablesRequest requestModel)
        {
            throw new NotImplementedException();
        }
    }
}
