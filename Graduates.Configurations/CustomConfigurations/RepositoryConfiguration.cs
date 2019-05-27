using Graduates.Core;
using Graduates.Core.Repositories;
using Graduates.Persistence;
using Graduates.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Graduates.Configurations.CustomConfigurations
{
    public class RepositoryConfiguration
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IInstitutionRepository, InstitutionRepository>();
            services.AddTransient<IFacultyRepository, FacultyRepository>();
            services.AddTransient<IDepartmentRepository, DepartmentRepository>();
            services.AddTransient<ICourseRepository, CourseRepository>();
            services.AddTransient<IProgramRepository, ProgramRepository>();
            services.AddTransient<ICandidateCertificateRepository, CandidateCertificateRepository>();
            services.AddTransient<ICandidateRepository, CandidateRepository>();
            services.AddTransient<INewsCategoryRepository, NewsCategoryRepository>();
            services.AddTransient<INewsRepository, NewsRepository>();
            services.AddTransient<IArticleCategoryRepository, ArticleCategoryRepository>();
            services.AddTransient<IArticleRepository, ArticleRepository>();
            services.AddTransient<IScholarshipRepository, ScholarshipRepository>();
            services.AddTransient<IJobRepository, JobRepository>();
            services.AddTransient<ISetupNameRepository, SetupNameRepository>();
            services.AddTransient<ISetupValueRepository, SetupValueRepository>();
        }
    }
}
