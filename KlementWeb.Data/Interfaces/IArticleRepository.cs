using KlementWeb.Data.Models;
using System.Collections.Generic;

namespace KlementWeb.Data.Interfaces
{
    public interface IArticleRepository : IRepository<Article>
    {
        int FindArticleIdByUrl(string url);
        List<Article> FindBySearchPhrase(string searchPhrase);
    }
}
