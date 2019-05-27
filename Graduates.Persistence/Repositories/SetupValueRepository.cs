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
    public class SetupValueRepository : Repository<SetupValue>, ISetupValueRepository
    {
        public SetupValueRepository(GraduatesContext context)
            : base(context)
        { }

        public GraduatesContext GraduatesContext
        {
            get { return Context as GraduatesContext; }
        }

        public bool IsExists(SetupValue obj)
        {
            SetupValue setupValue = null;
            try
            {
                setupValue = GraduatesContext.SetupValues.First<SetupValue>(m => m.Name == obj.Name && m.Parent.Id == obj.Parent.Id);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }

            return setupValue != null ? true : false;
        }

        public override IEnumerable<SetupValue> GetAll()
        {
            return GraduatesContext.SetupValues
                    .Include(m => m.SetupName)
                    .Include(m => m.Parent)
                    .Include(m => m.Parent.SetupName);
        }
    }
}
