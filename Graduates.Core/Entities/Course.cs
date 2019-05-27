using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Graduates.Core.Entities
{
    [Table("Course")]
    public class Course
    {
        public long Id { set; get; }
        public long InstitutionId { set; get; }
        public Institution Institution { set; get; }
        public long? FacultyId { set; get; }
        public Faculty Faculty { set; get; }
        public long? DepartmentId { set; get; }
        public Department Department { set; get; }
        public String Name { set; get; }
        public String Email { set; get; }
        public String Phone { set; get; }

        public virtual IList<Program> Programs { set; get; }
    }
}
