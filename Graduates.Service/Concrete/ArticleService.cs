using DataTables.AspNet.AspNetCore;
using DataTables.AspNet.Core;
using Graduates.Core;
using Graduates.Core.Entities;
using Graduates.Core.Enums;
using Graduates.Service.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Graduates.Service.Concrete
{
    public class ArticleService : IArticleService
    {
        private readonly IUnitOfWork _uow;

        public ArticleService(IUnitOfWork uow)
        {
            this._uow = uow;
        }
        public IEnumerable<Article> GetAll()
        {
            return _uow.Articles.GetAll();
        }

        public Article GetById(long Id)
        {
            return _uow.Articles.Get(Id);
        }

        public bool Remove(Article obj)
        {
            bool state = false;

            _uow.Articles.Remove(obj);
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

            var obj = _uow.Articles.Get(id);

            _uow.Articles.Remove(obj);
            int result = _uow.Complete();
            if (result > 0)
            {
                state = true;
            }
            return state;
        }

        public bool Save(Article obj, ref string message)
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

        private bool Add(Article obj, ref string message)
        {
            bool state = false;
            obj.Status = ((int)StatusEnum.UnApproved).ToString();

            // Check if there is an existing name
            if (!_uow.Articles.IsExists(obj))
            {
                _uow.Articles.Add(obj);
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

        private bool Update(long Id, Article obj)
        {
            bool state = false;

            var objEx = _uow.Articles.Get(Id);
            objEx = obj;
            objEx.Id = (int)Id;
            _uow.Articles.Update(Id, objEx);
            int result = _uow.Complete();
            if (result > 0)
            {
                state = true;
            }
            return state;
        }

        public DataTablesResponse SearchApi(IDataTablesRequest requestModel, StatusEnum status)
        {
            IQueryable<Article> query = null;

            switch (status)
            {
                case (StatusEnum.Approved):
                    query = _uow.Articles.GetAll().Where(m => m.Status == ((int)StatusEnum.Approved).ToString()).AsQueryable();
                    break;
                case (StatusEnum.UnApproved):
                    query = _uow.Articles.GetAll().Where(m => m.Status == ((int)StatusEnum.UnApproved).ToString()).AsQueryable();
                    break;
                case (StatusEnum.Rejected):
                    query = _uow.Articles.GetAll().Where(m => m.Status == ((int)StatusEnum.Rejected).ToString()).AsQueryable();
                    break;
            }

            var totalCount = query.Count();

            // Apply filters
            if (!string.IsNullOrEmpty(requestModel.Search.Value))
            {
                var value = requestModel.Search.Value.Trim();
                query = query.Where(p => p.Title.Contains(value)
                            || p.SubTitle.Contains(value));
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
                                      Title = tr.Title,
                                      SubTitle = tr.SubTitle,
                                      Category = tr.ArticleCategory.Name
                                  };
            try
            {
                var listValues = transformedData.ToList();
            }
            catch(Exception ce)
            {
                ce.ToString();
            }


            DataTablesResponse response = DataTablesResponse.Create(requestModel, totalCount, filteredCount, transformedData);
            return response;
        }

    }
}
