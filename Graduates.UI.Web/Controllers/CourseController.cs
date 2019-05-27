using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
    public class CourseController : Controller
    {
        private readonly ICourseService _courseService;
        private readonly IProgramService _programService;
        private readonly IMapper _mapper;

        public CourseController(ICourseService courseService, IMapper mapper, IProgramService programService)
        {
            this._courseService = courseService;
            this._programService = programService;
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
        [Consumes("application/json")]
        public IActionResult Create([FromBody]CourseVM data)
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
                //.DepartmentId = data.DepartmentId == 0 ? null : data.DepartmentId;
                var obj = _mapper.Map<Course>(data);
                if (_courseService.Save(obj, ref message))
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
            Course obj = _courseService.GetById(Id);
            
            for(int a = 0; a < obj.Programs.Count; ++a)
            {
                obj.Programs[a] = _programService.GetById(obj.Programs[a].Id);
            }
        
            var result = _mapper.Map<CourseVM>(obj);
            
            return Json(new { Course = result });
        }

        public IActionResult GetCoursesByDepartmentId(long departmentId)
        {
            var list = _courseService.GetCoursesByDepartmentId(departmentId);
            var courses = _mapper.Map<List<CourseVM>>(list);
            return Json(new { courses });
        }

        public IActionResult GetAll()
        {
            var list = _courseService.GetAll();
            var courses = _mapper.Map<List<CourseVM>>(list);
            return Json(new { courses });
        }

        public IActionResult Data(IDataTablesRequest requestModel)
        {
            DataTablesResponse response = _courseService.SearchApi(requestModel);
            DataTablesResponse responseTransformed = DataTablesResponse.Create(requestModel, response.TotalRecords, response.TotalRecordsFiltered, response.Data);
            return new DataTablesJsonResult(responseTransformed, true);
        }
    }
}