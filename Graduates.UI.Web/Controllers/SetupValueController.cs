using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataTables.AspNet.AspNetCore;
using DataTables.AspNet.Core;
using Graduates.Core.Entities;
using Graduates.Service.Abstract;
using Graduates.ViewModel.Dtos;
using Graduates.ViewModel.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Graduates.UI.Web.Controllers
{
    public class SetupValueController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ISetupValueService _setupValueService;
        private readonly ISetupNameService _setupNameService;

        public SetupValueController(ISetupValueService service, IMapper mapper, ISetupNameService setupNameService)
        {
            _setupValueService = service;
            _setupNameService = setupNameService;
            _mapper = mapper;
        }
        //
        public ActionResult Index(SetupValueParam param)
        {
            SetupName obj = _setupNameService.GetById(Convert.ToInt64(param.SetUpNameId));
            var result = _mapper.Map<SetupNameVM>(obj);

            param.Description = $"Add {result.Name}";

            return View(param);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Create([FromBody]SetupValueVM dto)
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

            var obj = _mapper.Map<SetupValue>(dto);
            obj.ParentId = obj.ParentId == 0 ? null : obj.ParentId;
            if (_setupValueService.Save(obj, ref message))
            {
                return Json(new { success = true, reason = string.Empty });
            }
            else
            {
                return Json(new { success = false, reason = message });
            }
        }

        public ActionResult Edit(int Id)
        {
            SetupValue obj = _setupValueService.GetById(Id);
            var result = _mapper.Map<SetupValueVM>(obj);
            return Json(new { setUpValue = result });
        }

        public ActionResult Get()
        {
            var allSetupNames = _setupNameService.GetAll().ToList();
            var listSetUpNames = _mapper.Map<List<SetupNameVM>>(allSetupNames);

            return Json(new { setUpNames = listSetUpNames });
        }

        public ActionResult GetParentSetupNameId(int ParentSetupNameId)
        {
            var parent = _setupNameService.GetById(ParentSetupNameId);
            List<SetupValueVM> listSetUpValues = new List<SetupValueVM>();

            if (ParentSetupNameId > 0)
            {
                var allSetUpValues = _setupValueService.GetAll().Where(m => m.SetupName.Id == parent.Id).ToList();
                listSetUpValues = _mapper.Map<List<SetupValueVM>>(allSetUpValues);
            }

            return Json(new { parentData = listSetUpValues });
        }

        public ActionResult GetSetupValues(SetupNameValueParam param)
        {
            List<SetupValue> allSetUpValues = null;
            List<SetupValueVM> listSetUpValues = null;

            if (param.ParentId == null)
            {
                allSetUpValues = _setupValueService.GetAll().Where(m => m.SetupName.Name.ToLower() == param.SetupName.ToLower()).ToList();
                listSetUpValues = _mapper.Map<List<SetupValueVM>>(allSetUpValues);
            }
            else
            {
                allSetUpValues = _setupValueService.GetAll().Where(m => m.SetupName.Name.ToLower() == param.SetupName.ToLower() && m.SetupName.Name == param.ParentId).ToList();
                listSetUpValues = _mapper.Map<List<SetupValueVM>>(allSetUpValues);
            }
            return Json(new { setUpValues = listSetUpValues });
        }

        public IActionResult Data(IDataTablesRequest requestModel, long? SetUpNameId)
        {
            DataTablesResponse response = _setupValueService.SearchApi(requestModel, SetUpNameId);
            DataTablesResponse responseTransformed = DataTablesResponse.Create(requestModel, response.TotalRecords, response.TotalRecordsFiltered, response.Data);
            return new DataTablesJsonResult(responseTransformed, true);
        }
    }
}