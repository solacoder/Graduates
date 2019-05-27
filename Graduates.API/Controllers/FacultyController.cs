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
    [Route("api/faculties")]
    [ApiController]
    public class FacultyController : ControllerBase
    {
        private readonly IFacultyService _facultyService;
        private readonly IMapper _mapper;

        public FacultyController(IFacultyService facultyService, IMapper mapper)
        {
            this._facultyService = facultyService;
            this._mapper = mapper;
        }

        // GET: api/Faculty
        [Route("")]
        public ActionResult<List<FacultyVM>> Get()
        {
            var data = _facultyService.GetAll();
            var faculties = _mapper.Map<List<FacultyVM>>(data);
            return faculties;
        }

        // GET: api/Faculty/{id}
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<FacultyVM> Get(long id)
        {
            var data = _facultyService.GetById(id);
            var obj = _mapper.Map<FacultyVM>(data);
            
            if (data == null)
            {
                return NotFound();
            }

            return obj;
        }
    }
}
