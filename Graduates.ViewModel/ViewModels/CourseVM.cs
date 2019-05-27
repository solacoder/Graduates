using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Graduates.ViewModel.ViewModels
{
    public class CourseVM
    {
        public string Id { set; get; }
        [Display(Name = "Institution")]
        public string InstitutionId { set; get; }

        [Display(Name = "Faculty")]
        public string FacultyId { set; get; }

        [Display(Name = "Department")]
        public string DepartmentId { set; get; }

        public string Name { set; get; }
        public string Email { set; get; }
        public string Phone { set; get; }

        public ProgramVM Program { set; get; } = new ProgramVM();

        public ICollection<ProgramVM> Programs { set; get; } = new List<ProgramVM>();
    }
}
