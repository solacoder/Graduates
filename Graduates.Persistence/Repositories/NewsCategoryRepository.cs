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
    public class NewsCategoryRepository : Repository<NewsCategory>, INewsCategoryRepository
    {
        public NewsCategoryRepository(GraduatesContext context)
            : base(context)
        { }

        public GraduatesContext GraduatesContext
        {
            get { return Context as GraduatesContext; }
        }

        public override IEnumerable<NewsCategory> GetAll()
        {
            return GraduatesContext.NewsCategories
                    .Include(m => m.News);
        }

        public bool IsExists(NewsCategory obj)
        {
            NewsCategory news = null;
            try
            {
                news = GraduatesContext.NewsCategories.First<NewsCategory>(m => m.Name == obj.Name);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }

            return news != null ? true : false;
        }

    }
}
