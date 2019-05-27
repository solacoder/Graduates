using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Graduates.ViewModel.ViewModels
{
    public class DepartmentVM
    {
        public long Id { set; get; }

        [Display(Name = "Institution")]
        public long InstitutionId { set; get; }

        [Display(Name = "Faculty")]
        public long FacultyId { set; get; }

        public String Name { set; get; }
        public String Email { set; get; }
        public String Phone { set; get; }
    }
}
