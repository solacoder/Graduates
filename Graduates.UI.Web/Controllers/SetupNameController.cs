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

namespace Graduates.UI.Web.Controllers
{
    public class SetupNameController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ISetupNameService _setupNameService;

        public SetupNameController(ISetupNameService setupNameService, IMapper mapper)
        {
            _mapper = mapper;
            _setupNameService = setupNameService;
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
        public IActionResult Create([FromBody]SetupNameVM data)
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
                var obj = _mapper.Map<SetupName>(data);
                if (_setupNameService.Save(obj, ref message))
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
            SetupName obj = _setupNameService.GetById(Id);
            var result = _mapper.Map<SetupNameVM>(obj);
            return Json(new { setupName = result });
        }

        public IActionResult GetParent(long ParentId)
        {
            var all = _setupNameService.GetAll().Where(m => m.ParentId == ParentId).ToList();
            var list = _mapper.Map<List<SetupNameVM>>(all);
            return Json(new { setUpNames = list });
        }

        public ActionResult GetAll()
        {
            var all = _setupNameService.GetAll().ToList();
            var list = _mapper.Map<List<SetupNameVM>>(all);
            return Json(new { setupNames = list });
        }

        public IActionResult Data(IDataTablesRequest requestModel)
        {
            DataTablesResponse response = _setupNameService.SearchApi(requestModel);
            DataTablesResponse responseTransformed = DataTablesResponse.Create(requestModel, response.TotalRecords, response.TotalRecordsFiltered, response.Data);
            return new DataTablesJsonResult(responseTransformed, true);
        }

       
    }
}