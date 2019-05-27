using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Graduates.Core.Entities
{
    [Table("NewsCategory")]
    public class NewsCategory
    {
        public long Id { set; get; }
        public string Name { set; get; }
        public string Description { set; get; }
        public bool IsActive { set; get; }

        public virtual IEnumerable<News> News { set; get; }
    }
}
