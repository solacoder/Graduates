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
    [Route("api/programs")]
    [ApiController]
    public class ProgramController : ControllerBase
    {
        private readonly IProgramService _programService;
        private readonly IMapper _mapper;

        public ProgramController(IProgramService programService, IMapper mapper)
        {
            this._programService = programService;
            this._mapper = mapper;
        }

        // GET: api/Program
        [Route("")]
        public ActionResult<List<ProgramVM>> Get()
        {
            var data = _programService.GetAll();
            var programs = _mapper.Map<List<ProgramVM>>(data);
            return programs;
        }

        // GET: api/Program/{id}
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ProgramVM> Get(long id)
        {
            var data = _programService.GetById(id);
            var obj = _mapper.Map<ProgramVM>(data);
            
            if (data == null)
            {
                return NotFound();
            }

            return obj;
        }
    }
}
