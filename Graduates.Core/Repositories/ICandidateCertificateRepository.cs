using Graduates.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Graduates.Core.Repositories
{
    public interface ICandidateCertificateRepository : IRepository<CandidateCertificate>
    {
        bool IsExists(CandidateCertificate obj);
    }
}
