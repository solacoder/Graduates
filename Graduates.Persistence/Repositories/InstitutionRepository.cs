using Graduates.Core.Entities;
using Graduates.Core.Repositories;
using Graduates.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Graduates.Persistence.Repositories
{
    public class InstitutionRepository : Repository<Institution>, IInstitutionRepository
    {
        public InstitutionRepository(GraduatesContext context)
            : base(context)
        { }

        public GraduatesContext GraduatesContext
        {
            get { return Context as GraduatesContext; }
        }

        public override IEnumerable<Institution> GetAll()
        {
            return GraduatesContext.Institutions
                    .Include(m => m.County)
                    .Include(m => m.OwnershipType)
                    .Include(m => m.Type);
        }

        public bool IsExists(Institution obj)
        {
            Institution institution = null;
            try
            {
                institution = GraduatesContext.Institutions.First<Institution>(m => m.Name == obj.Name);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }

            return institution != null ? true : false;
        }
    }
}
