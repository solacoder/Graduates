using Graduates.Core.Entities;
using Graduates.Service.Abstract;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graduates.Service.Concrete
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<ApplicationRole> _roleManager;

        public RoleService(RoleManager<ApplicationRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public IEnumerable<ApplicationRole> GetAll()
        {
            return _roleManager.Roles;
        }

        public ApplicationRole GetById(string Id)
        {
            return _roleManager.Roles.First<ApplicationRole>(m => m.Id == Id);
        }

        public async Task<bool> UpdateUser(ApplicationRole obj)
        {
            IdentityResult result = await _roleManager.UpdateAsync(obj);
            return result.Succeeded;
        }

    }
}
