using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataTables.AspNet.AspNetCore;
using DataTables.AspNet.Core;
using Graduates.Core.Entities;
using Graduates.Core.Enums;
using Graduates.Service.Abstract;
using Graduates.ViewModel.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Graduates.UI.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IRoleService _roleService;

        public UserController(IUserService userService, IRoleService roleService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
            _roleService = roleService;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]UserVM data)
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
                var obj = _mapper.Map<ApplicationUser>(data);

                if (await _userService.AddUser(obj, data.Role))
                {
                    return Json(new { success = true, reason = string.Empty });
                }
                else
                {
                    return Json(new { success = false, reason = "Unable to Submit Data" });
                }
            }
        }

        public IActionResult Edit(string Id)
        {
            var obj = _userService.GetById(Id);
            var user = _mapper.Map<ApplicationUser>(obj);
            //var roles = _roleService.
            return Json(new { user });
        }

        public IActionResult PendingApprovals()
        {
            return View("Index_Pending");
        }

        public IActionResult ApproveUser()
        {
            return View();
        }

        public IActionResult ApprovedUsers(IDataTablesRequest requestModel)
        {
            DataTablesResponse response = _userService.SearchApi(requestModel, StatusEnum.Approved);
            DataTablesResponse responseTransformed = DataTablesResponse.Create(requestModel, response.TotalRecords, response.TotalRecordsFiltered, response.Data);
            return new DataTablesJsonResult(responseTransformed, true);
        }

        public IActionResult UnApprovedUsers(IDataTablesRequest requestModel)
        {
            DataTablesResponse response = _userService.SearchApi(requestModel, StatusEnum.UnApproved);
            DataTablesResponse responseTransformed = DataTablesResponse.Create(requestModel, response.TotalRecords, response.TotalRecordsFiltered, response.Data);
            return new DataTablesJsonResult(responseTransformed, true);
        }

        public IActionResult PendingUsers(IDataTablesRequest requestModel)
        {
            DataTablesResponse response = _userService.SearchApi(requestModel, StatusEnum.UnApproved);
            DataTablesResponse responseTransformed = DataTablesResponse.Create(requestModel, response.TotalRecords, response.TotalRecordsFiltered, response.Data);
            return new DataTablesJsonResult(responseTransformed, true);
        }

    }
}
