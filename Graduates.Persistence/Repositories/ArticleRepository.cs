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
    public class ArticleRepository : Repository<Article>, IArticleRepository
    {
        public ArticleRepository(GraduatesContext context)
            : base(context)
        { }

        public GraduatesContext GraduatesContext
        {
            get { return Context as GraduatesContext; }
        }

        public override IEnumerable<Article> GetAll()
        {
            return GraduatesContext.Articles
                    .Include(m => m.ArticleCategory);
        }

        public bool IsExists(Article obj)
        {
            Article news = null;
            try
            {
                news = GraduatesContext.Articles.First<Article>(m => m.Title == obj.Title && m.SubTitle == obj.SubTitle);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }

            return news != null ? true : false;
        }
    }
}
