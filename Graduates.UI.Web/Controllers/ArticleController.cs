using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataTables.AspNet.AspNetCore;
using DataTables.AspNet.Core;
using Graduates.Core.Entities;
using Graduates.Core.Enums;
using Graduates.Service.Abstract;
using Graduates.UI.Web.Helpers;
using Graduates.ViewModel.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Graduates.Web.Controllers
{
    public class ArticleController : Controller
    {
        private readonly IArticleService _articleService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly IHostingEnvironment _env;

        public ArticleController(IArticleService articleService, IMapper mapper, IConfiguration config, IHostingEnvironment env)
        {
            this._articleService = articleService;
            this._mapper = mapper;
            this._config = config;
            this._env = env;
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
        public IActionResult Create([FromBody]ArticleVM data)
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
                var obj = _mapper.Map<Article>(data);

                string thumNailImagePath = Path.Combine(_config.GetValue<string>("ArticlesThumbNailImagesPath"), "thumbnail_" + data.Title);
                string bigImagePath = Path.Combine(_config.GetValue<string>("ArticlesBigImagesPath"), "bigimage_" + data.Title);

                obj.BigImagePath = UploadHelper.CreateFilePath(bigImagePath, data.BigImageFileType);
                obj.ThumbnailPath = UploadHelper.CreateFilePath(thumNailImagePath, data.ThumbNailFileType);

                string thumbNailImageFullPath = Path.Combine(_env.ContentRootPath, obj.ThumbnailPath);
                string bigImageFullPath = Path.Combine(_env.ContentRootPath, obj.BigImagePath);

                System.IO.File.WriteAllBytes(thumbNailImageFullPath, Convert.FromBase64String(UploadHelper.RemoveBase64StringHeader(data.ThumbNailImg)));
                System.IO.File.WriteAllBytes(bigImageFullPath, Convert.FromBase64String(UploadHelper.RemoveBase64StringHeader(data.BigImageImg)));

                if (_articleService.Save(obj, ref message))
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
            Article obj = _articleService.GetById(Id);
            var news = _mapper.Map<ArticleVM>(obj);

            string bigImageFullPath = Path.Combine(_env.ContentRootPath, obj.BigImagePath);
            string thumbNailImageFullPath = Path.Combine(_env.ContentRootPath, obj.ThumbnailPath);
           
            news.BigImageImg = FileReaderHelper.GetDocumentFromFile(bigImageFullPath, news.BigImageFileType);
            news.ThumbNailImg = FileReaderHelper.GetDocumentFromFile(thumbNailImageFullPath, news.ThumbNailFileType);

            return Json(new { news });
        }

        public IActionResult GetAll()
        {
            var list = _articleService.GetAll();
            var news = _mapper.Map<List<ArticleVM>>(list);
            return Json(new { news });
        }

        public IActionResult PendingApprovals()
        {
            return View("Index_Pending");
        }

        public IActionResult ApproveArticle()
        {
            return View();
        }


        public IActionResult ApprovedArticles(IDataTablesRequest requestModel)
        {
            DataTablesResponse response = _articleService.SearchApi(requestModel, StatusEnum.Approved);
            DataTablesResponse responseTransformed = DataTablesResponse.Create(requestModel, response.TotalRecords, response.TotalRecordsFiltered, response.Data);
            return new DataTablesJsonResult(responseTransformed, true);
        }

        public IActionResult UnApprovedArticles(IDataTablesRequest requestModel)
        {
            DataTablesResponse response = _articleService.SearchApi(requestModel, StatusEnum.UnApproved);
            DataTablesResponse responseTransformed = DataTablesResponse.Create(requestModel, response.TotalRecords, response.TotalRecordsFiltered, response.Data);
            return new DataTablesJsonResult(responseTransformed, true);
        }

        public IActionResult PendingArticles(IDataTablesRequest requestModel)
        {
            DataTablesResponse response = _articleService.SearchApi(requestModel, StatusEnum.UnApproved);
            DataTablesResponse responseTransformed = DataTablesResponse.Create(requestModel, response.TotalRecords, response.TotalRecordsFiltered, response.Data);
            return new DataTablesJsonResult(responseTransformed, true);
        }
    }
}