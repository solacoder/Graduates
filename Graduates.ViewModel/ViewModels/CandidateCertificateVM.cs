using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Graduates.ViewModel.ViewModels
{
    public class CandidateCertificateVM
    {
        public Int64 Id { set; get; }

        [Display(Name = "Certificate No")]
        public string CertificateNo { set; get; }

        [Display(Name = "Candidate")]
        public long CandidateId { set; get; }

        public CandidateVM Candidate { set; get; }

        public string FotoURL { set; get; }
        [Display(Name = "Year Obtainined")]
        public string YearObtained { set; get; }

        [Display(Name = "Institution")]
        public string InstitutionId { set; get; }
        [Display(Name = "Faculty")]
        public string FacultyId { set; get; }
        [Display(Name = "Department")]
        public string DepartmentId { set; get; }
        [Display(Name ="Grade")]
        public string GradeId { set; get; }
        [Display(Name = "Course")]
        public string CourseId { set; get; }

        [Display(Name = "Program")]
        public string ProgramId { set; get; }

        [Display(Name = "Student No")]
        public string StudentNo { set; get; }

        [Display(Name = "Certificate Title")]
        public string CertificateTitle { set; get; }

        public string Comment { set; get; }
    }
}
