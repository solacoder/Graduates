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
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;
        private readonly IMapper _mapper;

        public DepartmentController(IDepartmentService departmentService, IMapper mapper)
        {
            this._departmentService = departmentService;
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
        public IActionResult Create([FromBody]DepartmentVM data)
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
                var obj = _mapper.Map<Department>(data);
                if (_departmentService.Save(obj, ref message))
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
            Department obj = _departmentService.GetById(Id);
            var result = _mapper.Map<DepartmentVM>(obj);
            return Json(new { department = result });
        }

        public IActionResult GetAll()
        {
            var list = _departmentService.GetAll();
            var departments = _mapper.Map<List<DepartmentVM>>(list);
            return Json(new { departments });
        }

        public IActionResult GetDepartmentsByFacultyId(long facultyId)
        {
            var list = _departmentService.GetDepartmentByFacultyId(facultyId);
            var departments = _mapper.Map<List<DepartmentVM>>(list);
            return Json(new { departments });
        }



        public IActionResult Data(IDataTablesRequest requestModel)
        {
            DataTablesResponse response = _departmentService.SearchApi(requestModel);
            DataTablesResponse responseTransformed = DataTablesResponse.Create(requestModel, response.TotalRecords, response.TotalRecordsFiltered, response.Data);
            return new DataTablesJsonResult(responseTransformed, true);
        }
    }
}