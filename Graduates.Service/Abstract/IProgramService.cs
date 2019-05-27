using Graduates.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Graduates.Service.Abstract
{
    public interface IProgramService : IService<Program>, ITableService
    {
        IEnumerable<Program> GetProgramsByCourseId(long courseId);
    }
}
