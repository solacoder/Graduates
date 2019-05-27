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
    public class JobController : Controller
    {
        private readonly IJobService _jobService;
        private readonly IMapper _mapper;

        public JobController(IJobService jobService, IMapper mapper)
        {
            _jobService = jobService;
            _mapper = mapper;
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
        public IActionResult Create([FromBody]JobVM data)
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
                var obj = _mapper.Map<Job>(data);
                if (_jobService.Save(obj, ref message))
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
            Job obj = _jobService.GetById(Id);
            var result = _mapper.Map<JobVM>(obj);
            return Json(new { Job = result });
        }

        public IActionResult GetAll()
        {
            var list = _jobService.GetAll();
            var faculties = _mapper.Map<List<JobVM>>(list);
            return Json(new { faculties });
        }

        public IActionResult PendingJobs()
        {
            return View("Index_Pending");
        }

        public IActionResult ApproveJob()
        {
            return View();
        }

        public IActionResult ApprovedJobs(IDataTablesRequest requestModel)
        {
            DataTablesResponse response = _jobService.SearchApi(requestModel, StatusEnum.Approved);
            DataTablesResponse responseTransformed = DataTablesResponse.Create(requestModel, response.TotalRecords, response.TotalRecordsFiltered, response.Data);
            return new DataTablesJsonResult(responseTransformed, true);
        }

        public IActionResult UnApprovedJobs(IDataTablesRequest requestModel)
        {
            DataTablesResponse response = _jobService.SearchApi(requestModel, StatusEnum.UnApproved);
            DataTablesResponse responseTransformed = DataTablesResponse.Create(requestModel, response.TotalRecords, response.TotalRecordsFiltered, response.Data);
            return new DataTablesJsonResult(responseTransformed, true);
        }

        public IActionResult PendingJobs(IDataTablesRequest requestModel)
        {
            DataTablesResponse response = _jobService.SearchApi(requestModel, StatusEnum.UnApproved);
            DataTablesResponse responseTransformed = DataTablesResponse.Create(requestModel, response.TotalRecords, response.TotalRecordsFiltered, response.Data);
            return new DataTablesJsonResult(responseTransformed, true);
        }
    }
}