using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Graduates.Core.Entities
{
    [Table("SetupName")]
    public class SetupName
    {
        public long Id { set; get; }
        [Required]
        public string Name { set; get; }

        public long? ParentId { set; get; }
        public virtual SetupName Parent { set; get; }

        public bool DeleteFlag { set; get; }

        public virtual ICollection<SetupValue> SetupValues { get; set; }
    }
}
