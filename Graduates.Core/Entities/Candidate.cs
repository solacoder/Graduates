using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Graduates.Core.Entities
{
    [Table("Candidate")]
    public class Candidate
    {
        public long Id { set; get; }
        public string CandidateNo { set; get; }
        public string FirstName { set; get; }
        public string LastName { set; get; }
        public string MiddleName { set; get; }
        public string Email { set; get; }
        public string Phone { set; get; }
        public long SexId { set; get; }
        public virtual SetupValue Sex { set; get; }
        [Column(TypeName = "Date")]
        public DateTime DateOfBirth { set; get; }

        public long CountyId { set; get; }
        public virtual SetupValue County { set; get; }

        public string FotoURL { set; get; }

        public virtual ICollection<CandidateCertificate> CandidateCertificates { get; set; }

    }
}
