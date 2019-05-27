using DataTables.AspNet.AspNetCore;
using DataTables.AspNet.Core;
using Graduates.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Graduates.Service.Abstract
{
    public interface IRoleService
    {
        Task<bool> UpdateUser(ApplicationRole obj);
        ApplicationRole GetById(string Id);
        IEnumerable<ApplicationRole> GetAll();
    }
}
