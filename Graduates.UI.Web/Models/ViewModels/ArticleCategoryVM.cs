using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Graduates.UI.Web.Models.ViewModels
{
    public class ArticleCategoryVM
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public string Description { set; get; }
        public bool IsActive { set; get; }

        public IEnumerable<ArticleVM> Articles { set; get; }
    }
}
