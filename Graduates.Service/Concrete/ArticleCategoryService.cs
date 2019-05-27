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
    public class ArticleCategoryService : IArticleCategoryService
    {
        private readonly IUnitOfWork _uow;

        public ArticleCategoryService(IUnitOfWork uow)
        {
            this._uow = uow;
        }
        public IEnumerable<ArticleCategory> GetAll()
        {
            return _uow.ArticleCategories.GetAll();
        }

        public ArticleCategory GetById(long Id)
        {
            return _uow.ArticleCategories.Get(Id);
        }

        public bool Remove(ArticleCategory obj)
        {
            bool state = false;

            _uow.ArticleCategories.Remove(obj);
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

            var obj = _uow.ArticleCategories.Get(id);

            _uow.ArticleCategories.Remove(obj);
            int result = _uow.Complete();
            if (result > 0)
            {
                state = true;
            }
            return state;
        }

        public bool Save(ArticleCategory obj, ref string message)
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

        private bool Add(ArticleCategory obj, ref string message)
        {
            bool state = false;

            // Check if there is an existing name
            if (!_uow.ArticleCategories.IsExists(obj))
            {
                _uow.ArticleCategories.Add(obj);
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

        private bool Update(long Id, ArticleCategory obj)
        {
            bool state = false;

            var objEx = _uow.ArticleCategories.Get(Id);
            objEx = obj;
            objEx.Id = (int)Id;
            _uow.ArticleCategories.Update(Id, objEx);
            int result = _uow.Complete();
            if (result > 0)
            {
                state = true;
            }
            return state;
        }

        public DataTablesResponse SearchApi(IDataTablesRequest requestModel)
        {
            IQueryable<ArticleCategory> query = _uow.ArticleCategories.GetAll().AsQueryable();

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
                                      Article = tr.Articles.Count()
                                  };
            var listValues = transformedData.ToList();

            DataTablesResponse response = DataTablesResponse.Create(requestModel, totalCount, filteredCount, transformedData);
            return response;
        }
    }
}
