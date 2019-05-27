using AutoMapper;
using Graduates.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Graduates.ViewModel.ViewModels;

namespace Graduates.Configurations.CustomConfigurations
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<InstitutionVM, Institution>().ReverseMap();
            CreateMap<FacultyVM, Faculty>().ReverseMap();
            CreateMap<DepartmentVM, Department>().ReverseMap();
            CreateMap<CourseVM, Course>().ReverseMap();
            CreateMap<ProgramVM, Program>().ReverseMap();
            CreateMap<CandidateVM, Candidate>().ReverseMap();

            CreateMap<NewsCategoryVM, NewsCategory>().ReverseMap();
            CreateMap<NewsVM, News>().ReverseMap();

            CreateMap<ArticleCategoryVM, ArticleCategory>().ReverseMap();
            CreateMap<ArticleVM, Article>().ReverseMap();

            CreateMap<ScholarshipVM, Scholarship>().ReverseMap();
            CreateMap<JobVM, Job>().ReverseMap();

            CreateMap<UserVM, ApplicationUser>().ReverseMap();
            CreateMap<RoleVM, ApplicationRole>().ReverseMap();

            CreateMap<CandidateCertificateVM, CandidateCertificate>().ReverseMap();
            CreateMap<SetupNameVM, SetupName>().ReverseMap();
            CreateMap<SetupValueVM, SetupValue>().ReverseMap();
        }
    }
}
