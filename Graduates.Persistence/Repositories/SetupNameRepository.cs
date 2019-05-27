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
    public class SetupNameRepository : Repository<SetupName>, ISetupNameRepository
    {
        public SetupNameRepository(GraduatesContext context)
            : base(context)
        { }

        public GraduatesContext GraduatesContext
        {
            get { return Context as GraduatesContext; }
        }

        public bool IsExists(SetupName obj)
        {
            SetupName setupName = null;
            try
            {
                setupName = GraduatesContext.SetupNames.First<SetupName>(m => m.Name == obj.Name);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }

            return setupName != null ? true : false;
        }

        public override IEnumerable<SetupName> GetAll()
        {
            return GraduatesContext.SetupNames.Include(m => m.Parent);
        }
    }
}
