using System;
using System.Collections.Generic;
using System.Text;

namespace Graduates.ViewModel.ViewModels
{
    public class ScholarshipVM
    {
        public long Id { set; get; }
        public string Name { set; get; }
        public string Sponsor { set; get; }
        public string Audience { set; get; }
        public string Requirements { set; get; }
        public string Interests { set; get; } //study areas scholarship sponsors are interested in
        public string Website { set; get; }
        public string HowToApply { set; get; }
        public string Email { set; get; }
        public string Phone { set; get; }

        public string StartDate { set; get; }

        public string EndDate { set; get; }

        public string CreatedBy { set; get; }
        public string ApprovedBy { set; get; }
        public string DateCreated { set; get; }
        public string DateApproved { set; get; }
        public bool IsActive { set; get; }
    }
}
