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
    [Route("api/jobs")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private readonly ICourseService _jobService;
        private readonly IMapper _mapper;

        public JobController(ICourseService jobService, IMapper mapper)
        {
            this._jobService = jobService;
            this._mapper = mapper;
        }

        // GET: api/Course
        [Route("")]
        public ActionResult<List<CourseVM>> Get()
        {
            var data = _jobService.GetAll();
            var jobs = _mapper.Map<List<CourseVM>>(data);
            return jobs;
        }

        // GET: api/Course/{id}
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<CourseVM> Get(long id)
        {
            var data = _jobService.GetById(id);
            var obj = _mapper.Map<CourseVM>(data);
            
            if (data == null)
            {
                return NotFound();
            }

            return obj;
        }

    }
}
