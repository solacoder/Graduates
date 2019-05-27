using Graduates.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Graduates.Service.Abstract
{
    public interface IDepartmentService : IService<Department>, ITableService
    {
        IEnumerable<Department> GetDepartmentByFacultyId(long facultyId);
    }
}
