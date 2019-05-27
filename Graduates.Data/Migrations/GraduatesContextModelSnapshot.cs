﻿// <auto-generated />
using System;
using Graduates.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Graduates.Data.Migrations
{
    [DbContext(typeof(GraduatesContext))]
    partial class GraduatesContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.2.1-servicing-10028")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Graduates.Core.Entities.ApplicationRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("Description");

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Graduates.Core.Entities.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ActionType");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("DeleteFlag");

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<string>("Status");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Graduates.Core.Entities.Article", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ActionType");

                    b.Property<string>("ApprovedBy");

                    b.Property<long>("ArticleCategoryId");

                    b.Property<string>("BigImageFileType");

                    b.Property<string>("BigImagePath");

                    b.Property<string>("Content");

                    b.Property<string>("CreatedBy");

                    b.Property<string>("DateApproved");

                    b.Property<DateTime>("DatePublished");

                    b.Property<bool>("DeleteFlag");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsApproved");

                    b.Property<string>("Status");

                    b.Property<string>("SubTitle");

                    b.Property<string>("ThumbNailFileType");

                    b.Property<string>("ThumbnailPath");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("ArticleCategoryId");

                    b.ToTable("Article");
                });

            modelBuilder.Entity("Graduates.Core.Entities.ArticleCategory", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<bool>("IsActive");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("ArticleCategory");
                });

            modelBuilder.Entity("Graduates.Core.Entities.Candidate", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CandidateNo");

                    b.Property<long>("CountyId");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("Date");

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<string>("FotoURL");

                    b.Property<string>("LastName");

                    b.Property<string>("MiddleName");

                    b.Property<string>("Phone");

                    b.Property<long>("SexId");

                    b.HasKey("Id");

                    b.HasIndex("CountyId");

                    b.HasIndex("SexId");

                    b.ToTable("Candidate");
                });

            modelBuilder.Entity("Graduates.Core.Entities.CandidateCertificate", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("CandidateId");

                    b.Property<string>("CertificateNo");

                    b.Property<string>("CertificateTitle");

                    b.Property<string>("Comment");

                    b.Property<long>("CourseId");

                    b.Property<bool>("DeleteFlag");

                    b.Property<long>("DepartmentId");

                    b.Property<long?>("FacultyId");

                    b.Property<string>("FotoURL");

                    b.Property<long?>("GradeId");

                    b.Property<long>("InstitutionId");

                    b.Property<long?>("ProgramId");

                    b.Property<string>("StudentNo");

                    b.Property<string>("YearObtained");

                    b.HasKey("Id");

                    b.HasIndex("CandidateId");

                    b.HasIndex("CourseId");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("FacultyId");

                    b.HasIndex("GradeId");

                    b.HasIndex("InstitutionId");

                    b.HasIndex("ProgramId");

                    b.ToTable("CandidateCertificate");
                });

            modelBuilder.Entity("Graduates.Core.Entities.Course", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long?>("DepartmentId");

                    b.Property<string>("Email");

                    b.Property<long?>("FacultyId");

                    b.Property<long>("InstitutionId");

                    b.Property<string>("Name");

                    b.Property<string>("Phone");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("FacultyId");

                    b.HasIndex("InstitutionId");

                    b.ToTable("Course");
                });

            modelBuilder.Entity("Graduates.Core.Entities.Department", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<long>("FacultyId");

                    b.Property<long>("InstitutionId");

                    b.Property<string>("Name");

                    b.Property<string>("Phone");

                    b.HasKey("Id");

                    b.HasIndex("FacultyId");

                    b.HasIndex("InstitutionId");

                    b.ToTable("Department");
                });

            modelBuilder.Entity("Graduates.Core.Entities.Faculty", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<long>("InstitutionId");

                    b.Property<string>("Name");

                    b.Property<string>("Phone");

                    b.HasKey("Id");

                    b.HasIndex("InstitutionId");

                    b.ToTable("Faculty");
                });

            modelBuilder.Entity("Graduates.Core.Entities.Institution", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<long?>("CountryId");

                    b.Property<long?>("CountyId");

                    b.Property<string>("Email");

                    b.Property<string>("Name");

                    b.Property<long?>("OwnershipTypeId");

                    b.Property<string>("Phone");

                    b.Property<long?>("TypeId");

                    b.Property<string>("WebSite");

                    b.Property<string>("YearEstablished");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.HasIndex("CountyId");

                    b.HasIndex("OwnershipTypeId");

                    b.HasIndex("TypeId");

                    b.ToTable("Institution");
                });

            modelBuilder.Entity("Graduates.Core.Entities.Job", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ActionType");

                    b.Property<string>("ApprovedBy");

                    b.Property<string>("Company");

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime>("DateApproved");

                    b.Property<DateTime>("DateCreted");

                    b.Property<string>("Description");

                    b.Property<string>("Email");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("Date");

                    b.Property<bool>("IsActive");

                    b.Property<string>("Location");

                    b.Property<string>("Phone");

                    b.Property<string>("Requirements");

                    b.Property<string>("Responsibilities");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("Date");

                    b.Property<string>("Status");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("Jobs");
                });

            modelBuilder.Entity("Graduates.Core.Entities.News", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ActionType");

                    b.Property<string>("ApprovedBy");

                    b.Property<string>("BigImageFileType");

                    b.Property<string>("BigImagePath");

                    b.Property<string>("Content");

                    b.Property<string>("CreatedBy");

                    b.Property<string>("DateApproved");

                    b.Property<DateTime>("DatePublished");

                    b.Property<bool>("DeleteFlag");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsApproved");

                    b.Property<long>("NewsCategoryId");

                    b.Property<string>("Status");

                    b.Property<string>("SubTitle");

                    b.Property<string>("ThumbNailFileType");

                    b.Property<string>("ThumbnailPath");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("NewsCategoryId");

                    b.ToTable("News");
                });

            modelBuilder.Entity("Graduates.Core.Entities.NewsCategory", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<bool>("IsActive");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("NewsCategory");
                });

            modelBuilder.Entity("Graduates.Core.Entities.Program", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("CourseId");

                    b.Property<int>("Duration");

                    b.Property<long>("ProgramDurationUnitId");

                    b.Property<long>("QualificationTypeId");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.HasIndex("ProgramDurationUnitId");

                    b.HasIndex("QualificationTypeId");

                    b.ToTable("Program");
                });

            modelBuilder.Entity("Graduates.Core.Entities.Scholarship", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ActionType");

                    b.Property<string>("ApprovedBy");

                    b.Property<string>("Audience");

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime>("DateApproved");

                    b.Property<DateTime>("DateCreted");

                    b.Property<string>("Email");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("Date");

                    b.Property<string>("HowToApply");

                    b.Property<string>("Interests");

                    b.Property<bool>("IsActive");

                    b.Property<string>("Name");

                    b.Property<string>("Phone");

                    b.Property<string>("Requirements");

                    b.Property<string>("Sponsor");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("Date");

                    b.Property<string>("Status");

                    b.Property<string>("Website");

                    b.HasKey("Id");

                    b.ToTable("Scholarships");
                });

            modelBuilder.Entity("Graduates.Core.Entities.SetupName", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("DeleteFlag");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<long?>("ParentId");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("SetupName");
                });

            modelBuilder.Entity("Graduates.Core.Entities.SetupValue", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("DeleteFlag");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<long?>("ParentId");

                    b.Property<long>("SetupNameId");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.HasIndex("SetupNameId");

                    b.ToTable("SetupValue");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Graduates.Core.Entities.Article", b =>
                {
                    b.HasOne("Graduates.Core.Entities.ArticleCategory", "ArticleCategory")
                        .WithMany("Articles")
                        .HasForeignKey("ArticleCategoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Graduates.Core.Entities.Candidate", b =>
                {
                    b.HasOne("Graduates.Core.Entities.SetupValue", "County")
                        .WithMany()
                        .HasForeignKey("CountyId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Graduates.Core.Entities.SetupValue", "Sex")
                        .WithMany()
                        .HasForeignKey("SexId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Graduates.Core.Entities.CandidateCertificate", b =>
                {
                    b.HasOne("Graduates.Core.Entities.Candidate", "Candidate")
                        .WithMany("CandidateCertificates")
                        .HasForeignKey("CandidateId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Graduates.Core.Entities.Course", "Course")
                        .WithMany()
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Graduates.Core.Entities.Department", "Department")
                        .WithMany()
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Graduates.Core.Entities.Faculty", "Faculty")
                        .WithMany()
                        .HasForeignKey("FacultyId");

                    b.HasOne("Graduates.Core.Entities.SetupValue", "Grade")
                        .WithMany()
                        .HasForeignKey("GradeId");

                    b.HasOne("Graduates.Core.Entities.Institution", "Institution")
                        .WithMany()
                        .HasForeignKey("InstitutionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Graduates.Core.Entities.Program", "Program")
                        .WithMany()
                        .HasForeignKey("ProgramId");
                });

            modelBuilder.Entity("Graduates.Core.Entities.Course", b =>
                {
                    b.HasOne("Graduates.Core.Entities.Department", "Department")
                        .WithMany("Courses")
                        .HasForeignKey("DepartmentId");

                    b.HasOne("Graduates.Core.Entities.Faculty", "Faculty")
                        .WithMany()
                        .HasForeignKey("FacultyId");

                    b.HasOne("Graduates.Core.Entities.Institution", "Institution")
                        .WithMany()
                        .HasForeignKey("InstitutionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Graduates.Core.Entities.Department", b =>
                {
                    b.HasOne("Graduates.Core.Entities.Faculty", "Faculty")
                        .WithMany("Departments")
                        .HasForeignKey("FacultyId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Graduates.Core.Entities.Institution", "Institution")
                        .WithMany()
                        .HasForeignKey("InstitutionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Graduates.Core.Entities.Faculty", b =>
                {
                    b.HasOne("Graduates.Core.Entities.Institution", "Institution")
                        .WithMany("Faculties")
                        .HasForeignKey("InstitutionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Graduates.Core.Entities.Institution", b =>
                {
                    b.HasOne("Graduates.Core.Entities.SetupValue", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId");

                    b.HasOne("Graduates.Core.Entities.SetupValue", "County")
                        .WithMany()
                        .HasForeignKey("CountyId");

                    b.HasOne("Graduates.Core.Entities.SetupValue", "OwnershipType")
                        .WithMany()
                        .HasForeignKey("OwnershipTypeId");

                    b.HasOne("Graduates.Core.Entities.SetupValue", "Type")
                        .WithMany()
                        .HasForeignKey("TypeId");
                });

            modelBuilder.Entity("Graduates.Core.Entities.News", b =>
                {
                    b.HasOne("Graduates.Core.Entities.NewsCategory", "NewsCategory")
                        .WithMany("News")
                        .HasForeignKey("NewsCategoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Graduates.Core.Entities.Program", b =>
                {
                    b.HasOne("Graduates.Core.Entities.Course", "Course")
                        .WithMany("Programs")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Graduates.Core.Entities.SetupValue", "ProgramDurationUnit")
                        .WithMany()
                        .HasForeignKey("ProgramDurationUnitId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Graduates.Core.Entities.SetupValue", "QualificationType")
                        .WithMany()
                        .HasForeignKey("QualificationTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Graduates.Core.Entities.SetupName", b =>
                {
                    b.HasOne("Graduates.Core.Entities.SetupName", "Parent")
                        .WithMany()
                        .HasForeignKey("ParentId");
                });

            modelBuilder.Entity("Graduates.Core.Entities.SetupValue", b =>
                {
                    b.HasOne("Graduates.Core.Entities.SetupValue", "Parent")
                        .WithMany()
                        .HasForeignKey("ParentId");

                    b.HasOne("Graduates.Core.Entities.SetupName", "SetupName")
                        .WithMany("SetupValues")
                        .HasForeignKey("SetupNameId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Graduates.Core.Entities.ApplicationRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Graduates.Core.Entities.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Graduates.Core.Entities.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Graduates.Core.Entities.ApplicationRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Graduates.Core.Entities.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Graduates.Core.Entities.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
