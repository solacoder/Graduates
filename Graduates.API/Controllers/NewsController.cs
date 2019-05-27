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
    [Route("api/news")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _newsService;
        private readonly IMapper _mapper;

        public NewsController(INewsService newsService, IMapper mapper)
        {
            this._newsService = newsService;
            this._mapper = mapper;
        }

        // GET: api/News
        [Route("")]
        public ActionResult<List<NewsVM>> Get()
        {
            var data = _newsService.GetAll();
            var news = _mapper.Map<List<NewsVM>>(data);
            return news;
        }

        // GET: api/News/{id}
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<NewsVM> Get(long id)
        {
            var data = _newsService.GetById(id);
            var obj = _mapper.Map<NewsVM>(data);
            
            if (data == null)
            {
                return NotFound();
            }

            return obj;
        }
    }
}
