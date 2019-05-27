using DataTables.AspNet.AspNetCore;
using DataTables.AspNet.Core;
using Graduates.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Graduates.Service.Abstract
{
    public interface IService<T> where T : class
    {
        bool Save(T obj, ref string message);
        bool Remove(T obj);
        bool Remove(long id);
        T GetById(long Id);
        IEnumerable<T> GetAll();
    }

    public interface ITableService
    {
        DataTablesResponse SearchApi(IDataTablesRequest requestModel);
    }

    public interface ITableStatusService
    {
        DataTablesResponse SearchApi(IDataTablesRequest requestModel, StatusEnum status);
    }
}
