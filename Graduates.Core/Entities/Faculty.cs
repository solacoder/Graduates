using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Graduates.Core.Entities
{
    [Table("Faculty")]
    public class Faculty
    {
        public long Id { set; get; }
        public long InstitutionId { set; get; }
        public Institution Institution { set; get; }
        public String Name { set; get; }
        public String Phone { set; get; }
        public String Email { set; get; }

        public virtual ICollection<Department> Departments { get; set; }
    }
}
