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
    [Route("api/articles")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleService _articleService;
        private readonly IMapper _mapper;

        public ArticleController(IArticleService articleService, IMapper mapper)
        {
            this._articleService = articleService;
            this._mapper = mapper;
        }

        // GET: api/Article
        [Route("")]
        public ActionResult<List<ArticleVM>> Get()
        {
            var data = _articleService.GetAll();
            var institutions = _mapper.Map<List<ArticleVM>>(data);
            return institutions;
        }

        // GET: api/Article/{id}
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ArticleVM> Get(long id)
        {
            var data = _articleService.GetById(id);
            var obj = _mapper.Map<ArticleVM>(data);
            
            if (data == null)
            {
                return NotFound();
            }

            return obj;
        }
    }
}
