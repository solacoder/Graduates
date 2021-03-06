﻿using DataTables.AspNet.AspNetCore;
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
    public class ScholarshipService : IScholarshipService
    {
        private readonly IUnitOfWork _uow;

        public ScholarshipService(IUnitOfWork uow)
        {
            this._uow = uow;
        }
        public IEnumerable<Scholarship> GetAll()
        {
            return _uow.Scholarships.GetAll();
        }

        public Scholarship GetById(long Id)
        {
            return _uow.Scholarships.Get(Id);
        }

        public bool Remove(Scholarship obj)
        {
            bool state = false;

            _uow.Scholarships.Remove(obj);
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

            var obj = _uow.Scholarships.Get(id);

            _uow.Scholarships.Remove(obj);
            int result = _uow.Complete();
            if (result > 0)
            {
                state = true;
            }
            return state;
        }

        public bool Save(Scholarship obj, ref string message)
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

        private bool Add(Scholarship obj, ref string message)
        {
            bool state = false;

            // Check if there is an existing name
            if (!_uow.Scholarships.IsExists(obj))
            {
                _uow.Scholarships.Add(obj);
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

        private bool Update(long Id, Scholarship obj)
        {
            bool state = false;

            var objEx = _uow.Scholarships.Get(Id);
            objEx = obj;
            objEx.Id = (int)Id;
            _uow.Scholarships.Update(Id, objEx);
            int result = _uow.Complete();
            if (result > 0)
            {
                state = true;
            }
            return state;
        }

        public DataTablesResponse SearchApi(IDataTablesRequest requestModel, StatusEnum status)
        {
            IQueryable<Scholarship> query = null;

            switch (status)
            {
                case (StatusEnum.Approved):
                    query = _uow.Scholarships.GetAll().Where(m => m.Status == ((int)StatusEnum.Approved).ToString()).AsQueryable();
                    break;
                case (StatusEnum.UnApproved):
                    query = _uow.Scholarships.GetAll().Where(m => m.Status == ((int)StatusEnum.UnApproved).ToString()).AsQueryable();
                    break;
                case (StatusEnum.Rejected):
                    query = _uow.Scholarships.GetAll().Where(m => m.Status == ((int)StatusEnum.Rejected).ToString()).AsQueryable();
                    break;
            }

            var totalCount = query.Count();

            // Apply filters
            if (!string.IsNullOrEmpty(requestModel.Search.Value))
            {
                var value = requestModel.Search.Value.Trim();
                query = query.Where(p => p.Name.Contains(value) 
                                    || p.Interests.Contains(value) 
                                    || p.Audience.Contains(value));
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
                                      Website = tr.Website,
                                      Sponsor = tr.Sponsor,
                                      StartDate = tr.StartDate.Date.ToString("dd/MM/yyyy"),
                                      EndDate = tr.EndDate.Date.ToString("dd/MM/yyyy"),
                                      tr.Email,
                                      tr.Phone,
                                  };

            DataTablesResponse response = DataTablesResponse.Create(requestModel, totalCount, filteredCount, transformedData);
            return response;
        }
    }
}
