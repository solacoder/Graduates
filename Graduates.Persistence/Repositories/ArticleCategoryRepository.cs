using Graduates.Core.Entities;
using Graduates.Core.Repositories;
using Graduates.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Graduates.Persistence.Repositories
{
    public class ArticleCategoryRepository : Repository<ArticleCategory>, IArticleCategoryRepository
    {
        public ArticleCategoryRepository(GraduatesContext context)
            : base(context)
        { }

        public GraduatesContext GraduatesContext
        {
            get { return Context as GraduatesContext; }
        }

        public override IEnumerable<ArticleCategory> GetAll()
        {
            return GraduatesContext.ArticleCategories
                    .Include(m => m.Articles);
        }

        public bool IsExists(ArticleCategory obj)
        {
            ArticleCategory news = null;
            try
            {
                news = GraduatesContext.ArticleCategories.First<ArticleCategory>(m => m.Name == obj.Name);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }

            return news != null ? true : false;
        }

    }
}
