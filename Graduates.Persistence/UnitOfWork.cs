using Graduates.Core;
using Graduates.Core.Repositories;
using Graduates.Data;
using Graduates.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Graduates.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GraduatesContext _context;

        public UnitOfWork(GraduatesContext context)
        {
            _context = context;

            Institutions = new InstitutionRepository(_context);
            Faculties = new FacultyRepository(_context);
            Departments = new DepartmentRepository(_context);
            Courses = new CourseRepository(_context);
            Programs = new ProgramRepository(_context);
            News = new NewsRepository(_context);
            NewsCategories = new NewsCategoryRepository(_context);
            Articles = new ArticleRepository(_context);
            ArticleCategories = new ArticleCategoryRepository(_context);
            Candidates = new CandidateRepository(_context);
            CandidateCertificates = new CandidateCertificateRepository(_context);
            SetupNames = new SetupNameRepository(_context);
            SetupValues = new SetupValueRepository(_context);
            Scholarships = new ScholarshipRepository(_context);
            Jobs = new JobRepository(_context);
        }

        public IInstitutionRepository Institutions { get; private set; }
        public IFacultyRepository Faculties { get; private set; }
        public IDepartmentRepository Departments { get; private set; }
        public ICourseRepository Courses { get; private set; }
        public INewsCategoryRepository NewsCategories { get; private set; }
        public INewsRepository News { get; private set; }
        public IArticleCategoryRepository ArticleCategories { get; private set; }
        public IArticleRepository Articles { get; private set; }
        public IProgramRepository Programs { get; private set; }
        public ICandidateRepository Candidates { get; private set; }
        public ICandidateCertificateRepository CandidateCertificates { get; private set; }
        public ISetupNameRepository SetupNames { get; private set; }
        public ISetupValueRepository SetupValues { get; private set; }
        public IScholarshipRepository Scholarships {get; private set;}
        public IJobRepository Jobs { get; private set; }

        public int Complete()
        {
            try
            {
                return _context.SaveChanges();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return 0;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
