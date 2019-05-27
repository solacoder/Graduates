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
    public class ProgramController : Controller
    {
        private readonly IProgramService _programService;
        private readonly IMapper _mapper;

        public ProgramController(IMapper mapper, IProgramService programService)
        {
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
        public IActionResult Create([FromBody]ProgramVM data)
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
                var obj = _mapper.Map<Program>(data);
                if (_programService.Save(obj, ref message))
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
            Program obj = _programService.GetById(Id);
            
            //for(int a = 0; a < obj.Programs.Count; ++a)
            //{
            //    obj.Programs[a] = _programService.GetById(obj.Programs[a].Id);
            //}
        
            var program = _mapper.Map<ProgramVM>(obj);
            
            return Json(new { program });
        }

        public IActionResult GetProgramsByCourseId(long courseId)
        {
            var list =_programService.GetProgramsByCourseId(courseId);
            var programs = _mapper.Map<List<ProgramVM>>(list);
            return Json(new { programs });
        }

        public IActionResult GetAll()
        {
            var list = _programService.GetAll();
            var programs = _mapper.Map<List<ProgramVM>>(list);
            return Json(new { programs });
        }

        public IActionResult Data(IDataTablesRequest requestModel)
        {
            DataTablesResponse response = _programService.SearchApi(requestModel);
            DataTablesResponse responseTransformed = DataTablesResponse.Create(requestModel, response.TotalRecords, response.TotalRecordsFiltered, response.Data);
            return new DataTablesJsonResult(responseTransformed, true);
        }
    }
}