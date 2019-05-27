using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataTables.AspNet.AspNetCore;
using DataTables.AspNet.Core;
using Graduates.Core.Entities;
using Graduates.Core.Enums;
using Graduates.Service.Abstract;
using Graduates.ViewModel.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Graduates.Web.Controllers
{
    public class ScholarshipController : Controller
    {
        private readonly IScholarshipService _scholarshipService;
        private readonly IMapper _mapper;

        public ScholarshipController(IScholarshipService scholarshipService, IMapper mapper)
        {
            this._scholarshipService = scholarshipService;
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
        public IActionResult Create([FromBody]ScholarshipVM data)
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
                var obj = _mapper.Map<Scholarship>(data);
                if (_scholarshipService.Save(obj, ref message))
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
            Scholarship obj = _scholarshipService.GetById(Id);
            var scholarship = _mapper.Map<ScholarshipVM>(obj);
            scholarship.StartDate = obj.StartDate.Date.ToString("dd/MM/yyyy");
            scholarship.EndDate = obj.EndDate.Date.ToString("dd/MM/yyyy");
            return Json(new { scholarship });
        }

        public IActionResult GetAll()
        {
            var list = _scholarshipService.GetAll();
            var scholarships = _mapper.Map<List<ScholarshipVM>>(list);
            return Json(new { scholarships });
        }

        public IActionResult PendingApprovals()
        {
            return View("Index_Pending");
        }

        public IActionResult ApproveScholarship()
        {
            return View();
        }

        public IActionResult ApprovedScholarships(IDataTablesRequest requestModel)
        {
            DataTablesResponse response = _scholarshipService.SearchApi(requestModel, StatusEnum.Approved);
            DataTablesResponse responseTransformed = DataTablesResponse.Create(requestModel, response.TotalRecords, response.TotalRecordsFiltered, response.Data);
            return new DataTablesJsonResult(responseTransformed, true);
        }

        public IActionResult UnApprovedScholarships(IDataTablesRequest requestModel)
        {
            DataTablesResponse response = _scholarshipService.SearchApi(requestModel, StatusEnum.UnApproved);
            DataTablesResponse responseTransformed = DataTablesResponse.Create(requestModel, response.TotalRecords, response.TotalRecordsFiltered, response.Data);
            return new DataTablesJsonResult(responseTransformed, true);
        }

        public IActionResult PendingScholarships(IDataTablesRequest requestModel)
        {
            DataTablesResponse response = _scholarshipService.SearchApi(requestModel, StatusEnum.UnApproved);
            DataTablesResponse responseTransformed = DataTablesResponse.Create(requestModel, response.TotalRecords, response.TotalRecordsFiltered, response.Data);
            return new DataTablesJsonResult(responseTransformed, true);
        }
    }
}