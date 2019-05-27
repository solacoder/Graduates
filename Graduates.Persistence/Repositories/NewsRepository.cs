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
    public class NewsRepository : Repository<News>, INewsRepository
    {
        public NewsRepository(GraduatesContext context)
            : base(context)
        { }

        public GraduatesContext GraduatesContext
        {
            get { return Context as GraduatesContext; }
        }

        public override IEnumerable<News> GetAll()
        {
            return GraduatesContext.Newses
                    .Include(m => m.NewsCategory);
        }

        public bool IsExists(News obj)
        {
            News news = null;
            try
            {
                news = GraduatesContext.Newses.First<News>(m => m.Title == obj.Title && m.SubTitle == obj.SubTitle);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }

            return news != null ? true : false;
        }
    }
}
