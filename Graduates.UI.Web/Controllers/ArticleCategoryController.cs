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
    public class ArticleCategoryController : Controller
    {
        private readonly IArticleCategoryService _articleCategoryService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public ArticleCategoryController(IArticleCategoryService newsCategoryService, IMapper mapper)
        {
            this._articleCategoryService = newsCategoryService;
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
        public IActionResult Create([FromBody]ArticleCategoryVM data)
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
                var obj = _mapper.Map<ArticleCategory>(data);

                if (_articleCategoryService.Save(obj, ref message))
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
            ArticleCategory obj = _articleCategoryService.GetById(Id);
            var ArticleCategory = _mapper.Map<ArticleCategoryVM>(obj);
            return Json(new { ArticleCategory });
        }

        public IActionResult GetAll()
        {
            var list = _articleCategoryService.GetAll();
            var articleCategories = _mapper.Map<List<ArticleCategoryVM>>(list);
            return Json(new { articleCategories });
        }

       
        public IActionResult Data(IDataTablesRequest requestModel)
        {
            DataTablesResponse response = _articleCategoryService.SearchApi(requestModel);
            DataTablesResponse responseTransformed = DataTablesResponse.Create(requestModel, response.TotalRecords, response.TotalRecordsFiltered, response.Data);
            return new DataTablesJsonResult(responseTransformed, true);
        }
    }
}