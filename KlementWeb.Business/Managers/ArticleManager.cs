using KlementWeb.Business.Interfaces;
using KlementWeb.Data.Interfaces;
using KlementWeb.Data.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;

namespace KlementWeb.Business.Managers
{
    public class ArticleManager : IArticleManager
    {
        private readonly IArticleRepository articleRepository;
        private const string PathArticlePicture = "wwwroot/pictures/articles/";
        private IPictureManager pictureManager = new PictureManager(PathArticlePicture);
        private const int ArticlePictureMaxHeight = 1000;
        private const int ArticlePictureMaxWeight = 1000;

        public ArticleManager(IArticleRepository articleRepository) => this.articleRepository = articleRepository;

        public void AddOrUpdateArticle(Article article)
        {
            //SaveArticlePicture(article);
            articleRepository.Update(article);

        }

        public List<Article> GetAllArticles()
        {
            return articleRepository.ReturnEvery();
        }

        public void RemoveArticle(Article article)
        {
            try
            {
                articleRepository.Delete(article.Id);
                RemovePictureFile(article.Id);
            }
            catch (Exception)
            {

                articleRepository.Update(article);
            }
        }

        public void SaveArticlePicture(Article article, IFormFile picture)
        {
            if (article == null)
                
            if (articleRepository.FindArticleIdByUrl(article.Url) == 0)
                    throw new Exception("Url jednotlivých článků musí být unikátní - zvolte jinou Url!");

            pictureManager.SavePicture(picture,
                                    (articleRepository.FindArticleIdByUrl(article.Url).ToString() + "_picture"),
                                    PictureManager.PictureExtension.Jpeg,
                                    ArticlePictureMaxHeight, ArticlePictureMaxWeight
                                    );

            articleRepository.Update(article);
        }
        public List<Article> FindBySearchPhrase(string searchPhrase)
        {
            return articleRepository.FindBySearchPhrase(searchPhrase);
        }

        #region private function for pictures

        private void RemovePictureFile(int articleId)
        {
                string fileName = PathArticlePicture + articleId.ToString() + "_picture.jpeg";
            
            if (File.Exists(fileName))
                File.Delete(fileName);
        }
        #endregion
    }
}
