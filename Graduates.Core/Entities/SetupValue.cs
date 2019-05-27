using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Graduates.Core.Entities
{
    [Table("SetupValue")]
    public class SetupValue
    {
        public long Id { set; get; }

        [Required]
        public long SetupNameId { set; get; }
        public virtual SetupName SetupName { set; get; }

        [Required]
        public string Name { set; get; }

        public long? ParentId { set; get; }
        public virtual SetupValue Parent { set; get; }

        public bool DeleteFlag { set; get; }
    }
}
