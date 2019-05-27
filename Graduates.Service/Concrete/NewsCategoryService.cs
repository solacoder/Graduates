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
    public class NewsCategoryService : INewsCategoryService
    {
        private readonly IUnitOfWork _uow;

        public NewsCategoryService(IUnitOfWork uow)
        {
            this._uow = uow;
        }
        public IEnumerable<NewsCategory> GetAll()
        {
            return _uow.NewsCategories.GetAll();
        }

        public NewsCategory GetById(long Id)
        {
            return _uow.NewsCategories.Get(Id);
        }

        public bool Remove(NewsCategory obj)
        {
            bool state = false;

            _uow.NewsCategories.Remove(obj);
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

            var obj = _uow.NewsCategories.Get(id);

            _uow.NewsCategories.Remove(obj);
            int result = _uow.Complete();
            if (result > 0)
            {
                state = true;
            }
            return state;
        }

        public bool Save(NewsCategory obj, ref string message)
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

        private bool Add(NewsCategory obj, ref string message)
        {
            bool state = false;

            // Check if there is an existing name
            if (!_uow.NewsCategories.IsExists(obj))
            {
                _uow.NewsCategories.Add(obj);
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

        private bool Update(long Id, NewsCategory obj)
        {
            bool state = false;

            var objEx = _uow.NewsCategories.Get(Id);
            objEx = obj;
            objEx.Id = (int)Id;
            _uow.NewsCategories.Update(Id, objEx);
            int result = _uow.Complete();
            if (result > 0)
            {
                state = true;
            }
            return state;
        }

        public DataTablesResponse SearchApi(IDataTablesRequest requestModel)
        {
            IQueryable<NewsCategory> query = _uow.NewsCategories.GetAll().AsQueryable();

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
                                      Description = tr.Description,
                                      News = tr.News.Count()
                                  };
            var listValues = transformedData.ToList();

            DataTablesResponse response = DataTablesResponse.Create(requestModel, totalCount, filteredCount, transformedData);
            return response;
        }
    }
}
