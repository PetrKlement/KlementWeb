using KlementWeb.Data.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace KlementWeb.Business.Interfaces
{
    public interface IArticleManager
    {
        List<Article> GetAllArticles();

        void AddOrUpdateArticle(Article article);

        void RemoveArticle(Article article);

        void SaveArticlePicture(Article product, IFormFile picture);

        List<Article> FindBySearchPhrase(string searchPhrase);
    }
}
