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
    [Route("api/scholarships")]
    [ApiController]
    public class ScholarshipController : ControllerBase
    {
        private readonly IScholarshipService _scholarshipService;
        private readonly IMapper _mapper;

        public ScholarshipController(IScholarshipService scholarshipService, IMapper mapper)
        {
            this._scholarshipService = scholarshipService;
            this._mapper = mapper;
        }

        // GET: api/scholarship
        [Route("")]
        public ActionResult<List<ScholarshipVM>> Get()
        {
            var data = _scholarshipService.GetAll();
            var scholarships = _mapper.Map<List<ScholarshipVM>>(data);
            return scholarships;
        }

        // GET: api/scholarship/{id}
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ScholarshipVM> Get(long id)
        {
            var data = _scholarshipService.GetById(id);
            var obj = _mapper.Map<ScholarshipVM>(data);
            
            if (data == null)
            {
                return NotFound();
            }
            return obj;
        }
    }
}
