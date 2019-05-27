using DataTables.AspNet.AspNetCore;
using DataTables.AspNet.Core;
using Graduates.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Graduates.Service.Abstract
{
    public interface ISetupValueService : IService<SetupValue>, ITableService
    {
        DataTablesResponse SearchApi(IDataTablesRequest requestModel, long? SetUpNameId);
    }
}
