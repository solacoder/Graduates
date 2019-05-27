using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Graduates.Core.Entities
{
    public class Job
    {
        public long Id { set; get; }
        public string Title { set; get; }
        public string Company { set; get; }
        public string Location { set; get; }
        public string Description { set; get; }
        public string Responsibilities { set; get; }
        public string Requirements { set; get; }

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
        public bool IsActive { set; get; }
    }
}
