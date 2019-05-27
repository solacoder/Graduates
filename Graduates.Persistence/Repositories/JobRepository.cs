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
    public class JobRepository : Repository<Job>, IJobRepository
    {
        public JobRepository(GraduatesContext context)
            : base(context)
        { }

        public GraduatesContext GraduatesContext
        {
            get { return Context as GraduatesContext; }
        }

        public bool IsExists(Job obj)
        {
            Job candidate = null;
            try
            {
                candidate = GraduatesContext.Jobs.First<Job>(m => m.Title == obj.Title && m.Company == obj.Company);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }

            return candidate != null ? true : false;
        }

    }
}
