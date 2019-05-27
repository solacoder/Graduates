using Graduates.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Graduates.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IInstitutionRepository Institutions { get; }
        IFacultyRepository Faculties { get; }
        IDepartmentRepository Departments { get; }
        IProgramRepository Programs { get; }
        ICourseRepository Courses { get; }
        INewsCategoryRepository NewsCategories { get; }
        INewsRepository News { get; }
        IArticleCategoryRepository ArticleCategories { get; }
        IArticleRepository Articles { get; }
        IScholarshipRepository Scholarships { get; }
        IJobRepository Jobs { get; }

        ICandidateRepository Candidates { get; }
        ICandidateCertificateRepository CandidateCertificates { get; }
        ISetupNameRepository SetupNames { get; }
        ISetupValueRepository SetupValues { get; }

        int Complete();
    }
}
