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
    [Route("api/institutions")]
    [ApiController]
    public class InstitutionController : ControllerBase
    {
        private readonly IInstitutionService _institutionService;
        private readonly IMapper _mapper;

        public InstitutionController(IInstitutionService institutionService, IMapper mapper)
        {
            this._institutionService = institutionService;
            this._mapper = mapper;
        }

        // GET: api/institutions
        [Route("")]
        public ActionResult<List<InstitutionVM>> Get()
        {
            var data = _institutionService.GetAll();
            var institutions = _mapper.Map<List<InstitutionVM>>(data);
            return institutions;
        }

        // GET: api/institutions/{id}
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<InstitutionVM> Get(long id)
        {
            var data = _institutionService.GetById(id);
            var obj = _mapper.Map<InstitutionVM>(data);
            
            if (data == null)
            {
                return NotFound();
            }
            return obj;
        }
    }
}
