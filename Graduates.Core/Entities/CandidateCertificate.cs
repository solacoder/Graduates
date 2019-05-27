using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Graduates.Core.Entities
{
    [Table("CandidateCertificate")]
    public class CandidateCertificate
    {
        public long Id { set; get; }
        public string CertificateNo { set; get; }
        public string CertificateTitle { set; get; }

        public long CandidateId { set; get; }
        public Candidate Candidate { set; get; }

        public string FotoURL { set; get; }
        public string YearObtained { set; get; }

        public long? GradeId { set; get; }
        public virtual SetupValue Grade { set; get; }

        public long InstitutionId { set; get; }
        public Institution Institution { set; get; }

        public long? ProgramId { set; get; }
        public Program Program { set; get; }

        public long? FacultyId { set; get; }
        public Faculty Faculty { set; get; }
        public long DepartmentId { set; get; }
        public Department Department { set; get; }
        public long CourseId { set; get; }
        public Course Course { set; get; }
        public string StudentNo { set; get; }
        public string Comment { set; get; }
        public bool DeleteFlag { set; get; }
    }
}
