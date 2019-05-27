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
    public class NewsController : Controller
    {
        private readonly INewsService _newsService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly IHostingEnvironment _env;

        public NewsController(INewsService newsService, IMapper mapper, IConfiguration config, IHostingEnvironment env)
        {
            this._newsService = newsService;
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
        public IActionResult Create([FromBody]NewsVM data)
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
                var obj = _mapper.Map<News>(data);

                string thumNailImagePath = Path.Combine(_config.GetValue<string>("NewsThumbNailImagesPath"), "thumbnail_" + data.Title);
                string bigImagePath = Path.Combine(_config.GetValue<string>("NewsBigImagesPath"), "bigimage_" + data.Title);

                obj.BigImagePath = UploadHelper.CreateFilePath(bigImagePath, data.BigImageFileType);
                obj.ThumbnailPath = UploadHelper.CreateFilePath(thumNailImagePath, data.ThumbNailFileType);

                string thumbNailImageFullPath = Path.Combine(_env.ContentRootPath, obj.ThumbnailPath);
                string bigImageFullPath = Path.Combine(_env.ContentRootPath, obj.BigImagePath);

                System.IO.File.WriteAllBytes(thumbNailImageFullPath, Convert.FromBase64String(UploadHelper.RemoveBase64StringHeader(data.ThumbNailImg)));
                System.IO.File.WriteAllBytes(bigImageFullPath, Convert.FromBase64String(UploadHelper.RemoveBase64StringHeader(data.BigImageImg)));

                if (_newsService.Save(obj, ref message))
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
            News obj = _newsService.GetById(Id);
            var news = _mapper.Map<NewsVM>(obj);

            //constructing image paths
            string bigImageFullPath = Path.Combine(_env.ContentRootPath, obj.BigImagePath);
            string thumbNailImageFullPath = Path.Combine(_env.ContentRootPath, obj.ThumbnailPath);
           
            //creating Base 64 string for the images
            news.BigImageImg = FileReaderHelper.GetDocumentFromFile(bigImageFullPath, news.BigImageFileType);
            news.ThumbNailImg = FileReaderHelper.GetDocumentFromFile(thumbNailImageFullPath, news.ThumbNailFileType);

            return Json(new { news });
        }

        public IActionResult GetAll()
        {
            var list = _newsService.GetAll();
            var news = _mapper.Map<List<NewsVM>>(list);
            return Json(new { news });
        }

        public IActionResult PendingApprovals()
        {
            return View("Index_Pending");
        }

        public IActionResult ApproveNews()
        {
            return View();
        }


        public IActionResult ApprovedNews(IDataTablesRequest requestModel)
        {
            DataTablesResponse response = _newsService.SearchApi(requestModel, StatusEnum.Approved);
            DataTablesResponse responseTransformed = DataTablesResponse.Create(requestModel, response.TotalRecords, response.TotalRecordsFiltered, response.Data);
            return new DataTablesJsonResult(responseTransformed, true);
        }

        public IActionResult UnApprovedNews(IDataTablesRequest requestModel)
        {
            DataTablesResponse response = _newsService.SearchApi(requestModel, StatusEnum.UnApproved);
            DataTablesResponse responseTransformed = DataTablesResponse.Create(requestModel, response.TotalRecords, response.TotalRecordsFiltered, response.Data);
            return new DataTablesJsonResult(responseTransformed, true);
        }

        public IActionResult PendingNews(IDataTablesRequest requestModel)
        {
            DataTablesResponse response = _newsService.SearchApi(requestModel, StatusEnum.UnApproved);
            DataTablesResponse responseTransformed = DataTablesResponse.Create(requestModel, response.TotalRecords, response.TotalRecordsFiltered, response.Data);
            return new DataTablesJsonResult(responseTransformed, true);
        }


    }
}