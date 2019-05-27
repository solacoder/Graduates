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
    [Route("api/courses")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;
        private readonly IMapper _mapper;

        public CourseController(ICourseService courseService, IMapper mapper)
        {
            this._courseService = courseService;
            this._mapper = mapper;
        }

        // GET: api/Course
        [Route("")]
        public ActionResult<List<CourseVM>> Get()
        {
            var data = _courseService.GetAll();
            var courses = _mapper.Map<List<CourseVM>>(data);
            return courses;
        }

        // GET: api/Course/{id}
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<CourseVM> Get(long id)
        {
            var data = _courseService.GetById(id);
            var obj = _mapper.Map<CourseVM>(data);
            
            if (data == null)
            {
                return NotFound();
            }

            return obj;
        }

    }
}
