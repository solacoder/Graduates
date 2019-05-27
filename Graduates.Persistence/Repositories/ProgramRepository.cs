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
    public class ProgramRepository : Repository<Program>, IProgramRepository
    {
        public ProgramRepository(GraduatesContext context)
            : base(context)
        { }

        public GraduatesContext GraduatesContext
        {
            get { return Context as GraduatesContext; }
        }

        public bool IsExists(Program obj)
        {
            Program program = null;
            try
            {
                program = GraduatesContext.Programs.First<Program>(m => m.ProgramDurationUnitId == obj.ProgramDurationUnitId && m.QualificationTypeId == obj.QualificationTypeId);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }

            return program != null ? true : false;
        }

        public override Program Get(long Id)
        {
            return GraduatesContext.Programs
                    .Include(m => m.ProgramDurationUnit)
                    .Include(m => m.QualificationType)
                    .Single(m => m.Id == Id);
        }

        public override IEnumerable<Program> GetAll()
        {
            return GraduatesContext.Programs
                    .Include(m => m.ProgramDurationUnit)
                    .Include(m => m.QualificationType);   
        }
    }
}
