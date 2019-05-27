using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Graduates.Service.Abstract;
using Graduates.ViewModel.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Graduates.API.Controllers
{
    [Route("api/departments")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;
        private readonly IMapper _mapper;

        public DepartmentController(IDepartmentService departmentService, IMapper mapper)
        {
            this._departmentService = departmentService;
            this._mapper = mapper;
        }

        // GET: api/Department
        [Route("")]
        public ActionResult<List<DepartmentVM>> Get()
        {
            var data = _departmentService.GetAll();
            var departments = _mapper.Map<List<DepartmentVM>>(data);
            return departments;
        }

        // GET: api/Department/{id}
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<DepartmentVM> Get(long id)
        {
            var data = _departmentService.GetById(id);
            var obj = _mapper.Map<DepartmentVM>(data);
            
            if (data == null)
            {
                return NotFound();
            }

            return obj;
        }
    }
}
