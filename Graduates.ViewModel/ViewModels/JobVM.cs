using System;
using System.Collections.Generic;
using System.Text;

namespace Graduates.ViewModel.ViewModels
{
    public class JobVM
    {
        public long Id { set; get; }
        public string Title { set; get; }
        public string Company { set; get; }
        public string Location { set; get; }
        public string Description { set; get; }
        public string Responsibilities { set; get; }
        public string Requirements { set; get; }
        
        public string StartDate { set; get; }
        public string EndDate { set; get; }

        public string HowToApply { set; get; }
        public string CreatedBy { set; get; }
        public string ApprovedBy { set; get; }
        public string Status { set; get; }
        public string DateCreated { set; get; }
        public string DateApproved { set; get; }
        public string Email { set; get; }
        public string Phone { set; get; }
        public bool IsActive { set; get; }
    }
}
