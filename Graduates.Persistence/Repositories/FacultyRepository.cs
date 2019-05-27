using Graduates.Core.Entities;
using Graduates.Core.Repositories;
using Graduates.Data;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Graduates.Persistence.Repositories
{
    public class FacultyRepository : Repository<Faculty>, IFacultyRepository
    {
        public FacultyRepository(GraduatesContext context)
            : base(context)
        { }

        public GraduatesContext GraduatesContext
        {
            get { return Context as GraduatesContext; }
        }

        public bool IsExists(Faculty obj)
        {
            Faculty candidate = null;
            try
            {
                candidate = GraduatesContext.Faculties.First<Faculty>(m => m.Institution.Id == obj.InstitutionId && m.Name == obj.Name);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }

            return candidate != null ? true : false;
        }

        public override IEnumerable<Faculty> GetAll()
        {
            return GraduatesContext.Faculties
                .Include(m => m.Institution)
                .Include(m => m.Departments);
        }
    }
}
