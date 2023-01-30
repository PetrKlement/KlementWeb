using KlementWeb.Data.Interfaces;
using KlementWeb.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace KlementWeb.Data.Repositories
{
    public class ArticleRepository : ParentalRepository<Article>, IArticleRepository
    {
        public ArticleRepository(ApplicationDbContext context) : base(context)
        { }

        public int FindArticleIdByUrl(string url)
        {
            return dbSet.SingleOrDefault(x => x.Url == url).Id;
        }

        public List<Article> FindBySearchPhrase(string searchPhrase)
        {
            return string.IsNullOrEmpty(searchPhrase)
                ? dbSet.ToList()

                : dbSet.Where(x => x.Title.Contains(searchPhrase) ||
                                   x.Content.Contains(searchPhrase) ||
                                   x.Url.Contains(searchPhrase) ||
                                   x.Description.Contains(searchPhrase)
                             ).ToList();
        }
    }
}
