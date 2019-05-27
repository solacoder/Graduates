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
    public class CandidateCertificateRepository : Repository<CandidateCertificate>, ICandidateCertificateRepository
    {
        public CandidateCertificateRepository(GraduatesContext context)
            : base(context)
        { }

        public GraduatesContext GraduatesContext
        {
            get { return Context as GraduatesContext; }
        }

        public bool IsExists(CandidateCertificate obj)
        {
            CandidateCertificate candidateCertificate = null;
            try
            {
                candidateCertificate = GraduatesContext.CandidateCertificates.First<CandidateCertificate>(m => m.CertificateNo == obj.CertificateNo);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }

            return candidateCertificate != null ? true : false;
        }

        public override CandidateCertificate Get(long Id)
        {
            return GraduatesContext.CandidateCertificates
                    .Include(m => m.Candidate)
                    .Include(m => m.Institution)
                    .Include(m => m.Faculty)
                    .Include(m => m.Department)
                    .Include(m => m.Course)
                    .Include(m => m.Program).FirstOrDefault(m => m.Id == Id);
        }

        public override IEnumerable<CandidateCertificate> GetAll()
        {
            return GraduatesContext.CandidateCertificates
                    .Include(m => m.Institution)
                    .Include(m => m.Faculty)
                    .Include(m => m.Department)
                    .Include(m => m.Course)
                    .Include(m => m.Program);
        }
    }
}
