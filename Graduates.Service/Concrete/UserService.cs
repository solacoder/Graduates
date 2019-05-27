using DataTables.AspNet.AspNetCore;
using DataTables.AspNet.Core;
using Graduates.Core.Entities;
using Graduates.Core.Enums;
using Graduates.Service.Abstract;
using Graduates.Utility.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Graduates.Service.Concrete
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuraiton;

        public UserService(UserManager<ApplicationUser> userManager, IConfiguration configuraiton)
        {
            _userManager = userManager;
            _configuraiton = configuraiton;
        }

        public bool ActivateUser(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> AddUser(ApplicationUser obj, string role)
        {
            bool status = false;
            try
            {
                string password = PasswordGenerator.GeneratePassword(obj.FirstName, obj.LastName);
                IdentityResult result = await _userManager.CreateAsync(obj, password);
                if (result.Succeeded)
                {
                    
                    result = await _userManager.AddToRoleAsync(obj, role);
                }
            }catch(Exception ex)
            {
                ex.ToString();
            }
            return status;
        }

        public bool DisableUser(ApplicationUser obj)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ApplicationUser> GetAll()
        {
            throw new NotImplementedException();
        }

        public ApplicationUser GetById(string Id)
        {
            return _userManager.Users.FirstOrDefault(m => m.Id == Id);
        }

        public bool ResetUser(long id)
        {
            throw new NotImplementedException();
        }

        public DataTablesResponse SearchApi(IDataTablesRequest requestModel, StatusEnum status)
        {
            IQueryable<ApplicationUser> query = null;

            switch (status)
            {
                case (StatusEnum.Approved):
                    query = _userManager.Users.Where(m => m.Status == ((int)StatusEnum.Approved).ToString()).AsQueryable();
                    break;
                case (StatusEnum.UnApproved):
                    query = _userManager.Users.Where(m => m.Status == ((int)StatusEnum.UnApproved).ToString()).AsQueryable();
                    break;
                case (StatusEnum.Rejected):
                    query = _userManager.Users.Where(m => m.Status == ((int)StatusEnum.Rejected).ToString()).AsQueryable();
                    break;
            }
            

            var totalCount = query.Count();

            // Apply filters
            if (!string.IsNullOrEmpty(requestModel.Search.Value))
            {
                var value = requestModel.Search.Value.Trim();
                query = query.Where(p => p.UserName.Contains(value) 
                                || p.Email.Contains(value));
            }

            var filteredCount = query.Count();

            // Sorting
            var orderColums = requestModel.Columns.Where(x => x.Sort != null);

            //paging
            var data = query.OrderBy(orderColums).Skip(requestModel.Start).Take(requestModel.Length);

            var transformedData = from tr in data
                                  select new
                                  {
                                      Id = tr.Id,
                                      UserName = tr.UserName,
                                      FirstName = tr.FirstName,
                                      LastName = tr.LastName,
                                      PhoneNumber = tr.PhoneNumber,
                                      Email = tr.Email
                                  };

            DataTablesResponse response = DataTablesResponse.Create(requestModel, totalCount, filteredCount, transformedData);
            return response;
        }

        public bool UpdateUser(ApplicationUser obj)
        {
            throw new NotImplementedException();
        }
    }
}
