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
    public class CandidateRepository : Repository<Candidate>, ICandidateRepository
    {
        public CandidateRepository(GraduatesContext context)
            : base(context)
        { }

        public GraduatesContext GraduatesContext
        {
            get { return Context as GraduatesContext; }
        }

        public bool IsExists(Candidate obj)
        {
            Candidate candidate = null;
            try
            {
                candidate = GraduatesContext.Candidates.First<Candidate>(m => m.FirstName == obj.FirstName && m.LastName == obj.LastName);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }

            return candidate != null ? true : false;
        }

        public override Candidate Get(long Id)
        {
            return GraduatesContext.Candidates
                    .Include(m => m.County)
                    .Include(m => m.Sex).Single(m => m.Id == Id);
        }

        public override IEnumerable<Candidate> GetAll()
        {
            return GraduatesContext.Candidates
                    .Include(m => m.County)
                    .Include(m => m.Sex);
        }
    }
}
