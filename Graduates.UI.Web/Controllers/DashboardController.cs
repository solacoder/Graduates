using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Graduates.Service.Abstract;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Graduates.UI.Web.Controllers
{
    public class DashBoardController : Controller
    {
        private readonly IDashBoardService _dashBoardService;

        public DashBoardController(IDashBoardService dashBoardService)
        {
            this._dashBoardService = dashBoardService;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            var dashBoardItems = this._dashBoardService.GetDashBoardItems();
            return Json(new { dashBoardItems });
        }
    }
}
