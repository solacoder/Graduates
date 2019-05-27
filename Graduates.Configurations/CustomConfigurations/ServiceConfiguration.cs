using Graduates.Core;
using Graduates.Persistence;
using Graduates.Service.Abstract;
using Graduates.Service.Concrete;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Graduates.Configurations.CustomConfigurations
{
    public class ServiceConfiguration
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddTransient<IInstitutionService, InstitutionService>();
            services.AddTransient<IFacultyService, FacultyService>();
            services.AddTransient<IDepartmentService, DepartmentService>();
            services.AddTransient<ICourseService, CourseService>();
            services.AddTransient<IProgramService, ProgramService>();
            services.AddTransient<ICandidateCertificateService, CandidateCertificateService>();
            services.AddTransient<ICandidateService, CandidateService>();
            services.AddTransient<INewsCategoryService, NewsCategoryService>();
            services.AddTransient<INewsService, NewsService>();
            services.AddTransient<IArticleCategoryService, ArticleCategoryService>();
            services.AddTransient<IArticleService, ArticleService>();
            services.AddTransient<IScholarshipService, ScholarshipService>();
            services.AddTransient<IJobService, JobService>();
            services.AddTransient<IDashBoardService, DashBoardService>();
            services.AddTransient<ISetupNameService, SetupNameService>();
            services.AddTransient<ISetupValueService, SetupValueService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IRoleService, RoleService>();
        }
    }
}
