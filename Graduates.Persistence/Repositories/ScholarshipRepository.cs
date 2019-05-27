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
    public class ScholarshipRepository : Repository<Scholarship>, IScholarshipRepository
    {
        public ScholarshipRepository(GraduatesContext context)
            : base(context)
        { }

        public GraduatesContext GraduatesContext
        {
            get { return Context as GraduatesContext; }
        }

        public bool IsExists(Scholarship obj)
        {
            Scholarship candidate = null;
            try
            {
                candidate = GraduatesContext.Scholarships.First<Scholarship>(m => m.Name == obj.Name);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }

            return candidate != null ? true : false;
        }


    }
}
