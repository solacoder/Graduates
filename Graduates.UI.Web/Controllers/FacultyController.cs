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
    public class FacultyController : Controller
    {
        private readonly IFacultyService _facultyService;
        private readonly IMapper _mapper;

        public FacultyController(IFacultyService facultyService, IMapper mapper)
        {
            _facultyService = facultyService;
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
        public IActionResult Create([FromBody]FacultyVM data)
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
                var obj = _mapper.Map<Faculty>(data);
                if (_facultyService.Save(obj, ref message))
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
            Faculty obj = _facultyService.GetById(Id);
            var result = _mapper.Map<FacultyVM>(obj);
            return Json(new { Faculty = result });
        }

        public IActionResult GetAll()
        {
            var list = _facultyService.GetAll();
            var faculties = _mapper.Map<List<FacultyVM>>(list);
            return Json(new { faculties });
        }

        public IActionResult GetFacultiesByInstitutionId(long InstitutionId)
        {
            var list = _facultyService.GetByInstitutionId(InstitutionId);
            var faculties = _mapper.Map<List<FacultyVM>>(list);
            return Json(new { faculties });
        }

        public IActionResult Data(IDataTablesRequest requestModel)
        {
            DataTablesResponse response = _facultyService.SearchApi(requestModel);
            DataTablesResponse responseTransformed = DataTablesResponse.Create(requestModel, response.TotalRecords, response.TotalRecordsFiltered, response.Data);
            return new DataTablesJsonResult(responseTransformed, true);
        }
    }
}