using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Graduates.UI.Web.Models.ViewModels
{
    public class FacultyVM
    {
        public Int64 Id { set; get; }

        [Display(Name = "Institution")]
        public Int64 InstitutionId { set; get; }

        public String Name { set; get; }
        public String Phone { set; get; }
        public String Email { set; get; }
    }
}
