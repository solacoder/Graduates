using DataTables.AspNet.AspNetCore;
using DataTables.AspNet.Core;
using Graduates.Core.Entities;
using Graduates.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Graduates.Service.Abstract
{
    public interface IUserService : ITableStatusService
    {
        Task<bool> AddUser(ApplicationUser obj, string role);
        bool UpdateUser(ApplicationUser obj);
        bool DisableUser(ApplicationUser obj);
        bool ActivateUser(long id);
        bool ResetUser(long id);
        ApplicationUser GetById(string Id);
        IEnumerable<ApplicationUser> GetAll();
    }
}
