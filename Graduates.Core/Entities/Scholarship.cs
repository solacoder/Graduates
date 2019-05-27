using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Graduates.Core.Entities
{
    public class Scholarship
    {
        public long Id { set; get; }
        public string Name { set; get; }
        public string Sponsor { set; get; }
        public string Website { set; get; }
        public string Audience { set; get; }
        public string Requirements { set; get; }
        public string Interests { set; get; } //study areas scholarship sponsors are interested in
        public string HowToApply { set; get; }
        [Column(TypeName = "Date")]
        public DateTime StartDate { set; get; }
        [Column(TypeName = "Date")]
        public DateTime EndDate { set; get; }
        public string CreatedBy { set; get; }
        public string ApprovedBy { set; get; }
        public string Status { set; get; }
        public string ActionType { set; get; }

        public DateTime DateCreted { set; get; }

        public DateTime DateApproved { set; get; }
        public string Email { set; get; }
        public string Phone { set; get; }
        public bool IsActive {set; get;}
    }
}
