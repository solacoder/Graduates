using Graduates.Core.Entities;
using Graduates.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Graduates.UI.Web.CustomConfigurations
{
    public static class DbInitializer
    {
        public static async Task InitializeAsync(GraduatesContext context, IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            string[] roleNames = { "Super Admin", "Admin", "Inputter", "Reviewer", "Authorizer", "Editor" };
            IdentityResult roleResult;
            foreach(var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    roleResult = await roleManager.CreateAsync(new ApplicationRole(roleName)); 
                }
            }
        } 
    }
}
