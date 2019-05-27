using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Graduates.Core.Entities;

namespace Graduates.Data
{
    public class GraduatesContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public GraduatesContext(DbContextOptions<GraduatesContext> options) : base(options)
        {
        }

        public virtual DbSet<Institution> Institutions { get; set; }
        public virtual DbSet<Faculty> Faculties { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Program> Programs { get; set; }
        public virtual DbSet<NewsCategory> NewsCategories { get; set; }
        public virtual DbSet<News> Newses { get; set; }
        public virtual DbSet<ArticleCategory> ArticleCategories { get; set; }
        public virtual DbSet<Article> Articles { get; set; }
        public virtual DbSet<Candidate> Candidates { get; set; }
        public virtual DbSet<CandidateCertificate> CandidateCertificates { get; set; }
        public virtual DbSet<SetupName> SetupNames { get; set; }
        public virtual DbSet<SetupValue> SetupValues { get; set; }
        public virtual DbSet<Scholarship> Scholarships { get; set; }
        public virtual DbSet<Job>  Jobs { get; set; }
    }
}
