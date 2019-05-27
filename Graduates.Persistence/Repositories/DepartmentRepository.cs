using Graduates.Core.Entities;
using Graduates.Core.Repositories;
using Graduates.Data;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;

namespace Graduates.Persistence.Repositories
{
    public class DepartmentRepository : Repository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(GraduatesContext context)
            : base(context)
        { }

        public GraduatesContext GraduatesContext
        {
            get { return Context as GraduatesContext; }
        }

        public bool IsExists(Department obj)
        {
            Department department = null;
            try
            {
                department = GraduatesContext.Departments.First<Department>(m => m.Institution.Id == obj.InstitutionId
                                                                            && m.Faculty.Id == obj.FacultyId 
                                                                            && m.Name == obj.Name);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }

            return department != null ? true : false;
        }

        public override IEnumerable<Department> GetAll()
        {
            return GraduatesContext.Departments
                    .Include(m => m.Institution)
                    .Include(m => m.Faculty);
        }
    }
}
