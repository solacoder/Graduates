using Graduates.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Graduates.Service.Abstract
{
    public interface IFacultyService : IService<Faculty>, ITableService
    {
        IEnumerable<Faculty> GetByInstitutionId(long InstitutionId);
    }
}
