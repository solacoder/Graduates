using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataTables.AspNet.AspNetCore;
using DataTables.AspNet.Core;
using Graduates.Core.Entities;
using Graduates.Service.Abstract;
using Graduates.ViewModel.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Graduates.Web.Controllers
{
    public class NewsCategoryController : Controller
    {
        private readonly INewsCategoryService _newsCategoryService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public NewsCategoryController(INewsCategoryService newsCategoryService, IMapper mapper)
        {
            this._newsCategoryService = newsCategoryService;
            this._mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create([FromBody]NewsCategoryVM data)
        {
            string message = string.Empty;

            string validationErrors = string.Join(",", ModelState.Values.Where(E => E.Errors.Count > 0)
                    .SelectMany(E => E.Errors)
                    .Select(E => E.ErrorMessage)
                    .ToArray());

            if (!ModelState.IsValid)
            {
                return Json(new { success = false, reason = "Validation Failed. \n" + validationErrors });
            }
            else
            {
                var obj = _mapper.Map<NewsCategory>(data);

                if (_newsCategoryService.Save(obj, ref message))
                {
                    return Json(new { success = true, reason = string.Empty });
                }
                else
                {
                    return Json(new { success = false, reason = message });
                }
            }
        }

        public IActionResult Edit(long Id)
        {
            NewsCategory obj = _newsCategoryService.GetById(Id);
            var NewsCategory = _mapper.Map<NewsCategoryVM>(obj);
            return Json(new { NewsCategory });
        }

        public IActionResult GetAll()
        {
            var list = _newsCategoryService.GetAll();
            var newsCategories = _mapper.Map<List<NewsCategoryVM>>(list);
            return Json(new { newsCategories });
        }

       
        public IActionResult Data(IDataTablesRequest requestModel)
        {
            DataTablesResponse response = _newsCategoryService.SearchApi(requestModel);
            DataTablesResponse responseTransformed = DataTablesResponse.Create(requestModel, response.TotalRecords, response.TotalRecordsFiltered, response.Data);
            return new DataTablesJsonResult(responseTransformed, true);
        }
    }
}