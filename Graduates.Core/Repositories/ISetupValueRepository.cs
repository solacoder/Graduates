using Graduates.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Graduates.Core.Repositories
{
    public interface ISetupValueRepository : IRepository<SetupValue>
    {
        bool IsExists(SetupValue obj);
    }
}
