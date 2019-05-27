using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Graduates.UI.Web.Models.ViewModels
{
    public class CandidateVM
    {
        public Int64 Id { set; get; }

        [Display(Name = "Candidate No")]
        public string CandidateNo { set; get; }
        public string FirstName { set; get; }
        public string LastName { set; get; }
        public string MiddleName { set; get; }
        public string Email { set; get; }
        public string Phone { set; get; }
        [Display(Name ="Sex")]
        public string SexId { set; get; }
        public string SexName { set; get; }

        [Display(Name = "Date Of Birth")]
        public string DateOfBirth { set; get; }

        [Display(Name = "County of Origin")]
        public long CountyId { set; get; }

        public string FotoURL { set; get; }
    }

}
