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

namespace Graduates.Web.Controllers
{
    public class CandidateController : Controller
    {
        private readonly ICandidateService _candidateService;
        private readonly IMapper _mapper;

        public CandidateController(ICandidateService candidateService, IMapper mapper)
        {
            this._candidateService = candidateService;
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
        public IActionResult Create([FromBody]CandidateVM data)
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
                var obj = _mapper.Map<Candidate>(data);
                if (_candidateService.Save(obj, ref message))
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
            Candidate obj = _candidateService.GetById(Id);
            var result = _mapper.Map<CandidateVM>(obj);
            result.DateOfBirth = obj.DateOfBirth.Date.ToString("dd/MM/yyyy");
            return Json(new { Candidate = result });
        }

        public IActionResult GetAll()
        {
            var list = _candidateService.GetAll();
            var faculties = _mapper.Map<List<CandidateVM>>(list);
            return Json(new { faculties });
        }

        public IActionResult Data(IDataTablesRequest requestModel)
        {
            DataTablesResponse response = _candidateService.SearchApi(requestModel);
            DataTablesResponse responseTransformed = DataTablesResponse.Create(requestModel, response.TotalRecords, response.TotalRecordsFiltered, response.Data);
            return new DataTablesJsonResult(responseTransformed, true);
        }
    }
}