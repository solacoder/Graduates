using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Graduates.Core.Entities
{
    [Table("Department")]
    public class Department
    {
        public long Id { set; get; }

        public long InstitutionId { set; get; }
        public Institution Institution { set; get; }

        public Faculty Faculty { set; get; }
        public long FacultyId { set; get; }

        public String Name { set; get; }
        public String Email { set; get; }
        public String Phone { set; get; }

        public virtual ICollection<Course> Courses { get; set; }
    }
}
