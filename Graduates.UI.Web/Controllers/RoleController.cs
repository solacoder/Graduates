using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataTables.AspNet.AspNetCore;
using DataTables.AspNet.Core;
using Graduates.Core.Entities;
using Graduates.Service.Abstract;
using Graduates.ViewModel.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Graduates.UI.Web.Controllers
{
    public class RoleController : Controller
    {
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;

        public RoleController(IRoleService roleService, IMapper mapper)
        {
            _roleService = roleService;
            _mapper = mapper;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            var data = _roleService.GetAll();
            var roles = _mapper.Map<IEnumerable<ApplicationRole>>(data);
            return Json(new { roles });
        }

        public async Task<IActionResult> Update(RoleVM data)
        {
            string validationErrors = string.Join(",", ModelState.Values.Where(E => E.Errors.Count > 0)
                    .SelectMany(E => E.Errors)
                    .Select(E => E.ErrorMessage)
                    .ToArray());

            if (!ModelState.IsValid)
            {
                return Json(new { success = false, reason = "Validation Failed. \n" + validationErrors });
            }
            else
            {
                var obj = _mapper.Map<ApplicationRole>(data);

                if (await _roleService.UpdateUser(obj))
                {
                    return Json(new { success = true });
                }
                else
                {
                    return Json(new { success = false});
                }
            }
        }
    }
}
