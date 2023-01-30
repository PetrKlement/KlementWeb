using System.Linq;
using Microsoft.AspNetCore.Mvc;
using KlementWeb.Data.Models;
using KlementWeb.Classes;
using KlementWeb.Business.Interfaces;
using KlementWeb.Models.ArticleViewModels;
using Microsoft.AspNetCore.Authorization;

namespace KlementWeb.Controllers
{
    [ExceptionsToMessageFilterAttribute]
    [AutoValidateAntiforgeryToken]
    public class ArticleController : Controller
    {
        private readonly IArticleManager articleManager;

        public ArticleController(IArticleManager articleManager) => this.articleManager = articleManager;

        [HttpGet]
        public IActionResult Index()
        {
            return View(articleManager.GetAllArticles());
        }

        [HttpPost]
        public IActionResult Index(string searchPhrase)
        {
            if (!string.IsNullOrEmpty(searchPhrase))
                ViewData["SearchPhrase"] = searchPhrase;
            if (searchPhrase != null) // if user search article
            {
                return View(articleManager.FindBySearchPhrase(searchPhrase).ToList());
            }
            else
            {
                return View(articleManager.GetAllArticles());
            }  
        }

        [AllowAnonymous]
        public IActionResult Details(string url)
        {
            if (url == null)
                return NotFound();

            Article article = articleManager.GetAllArticles().Where(x => x.Url == url).FirstOrDefault();
            ArticleViewModel model = new ArticleViewModel { Id = article.Id, Title = article.Title, Url = article.Url, Description = article.Description, Content = article.Content,  };
            if (model == null)
                return NotFound();

            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            var model = new ArticleViewModel();
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(ArticleViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            Article article = new Article { Title = model.Title, Url = model.Url, Description = model.Description, Content = model.Content };
            articleManager.AddOrUpdateArticle(article);
            articleManager.SaveArticlePicture(article, model.TitlePicture);
            this.AddFlashMessage(new FlashMessage("Článek byl přidán", FlashMessageType.Success));
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(string? url)
        {
            if (url == null)
            {
                return NotFound();
            }

            Article article = articleManager.GetAllArticles().Where(x => x.Url == url).SingleOrDefault();

            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id, [Bind("Id,Title,Url,Description,Content")] Article article)
        {
            if (id != article.Id)
                return NotFound();

            if (!ModelState.IsValid)
                return View(article);

            articleManager.AddOrUpdateArticle(article);
            this.AddFlashMessage(new FlashMessage("Článek byl upraven", FlashMessageType.Success));

            return RedirectToAction("Details", new { id = article.Id });
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Article article = articleManager.GetAllArticles().Where(x => x.Id == id).SingleOrDefault();

            if (article == null)
                return NotFound();

            return View(article);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            Article article = articleManager.GetAllArticles().Where(x => x.Id == id).SingleOrDefault();

            if (article == null)
                return NotFound();

            articleManager.RemoveArticle(article);
            this.AddFlashMessage(new FlashMessage("Článek je odstraněn", FlashMessageType.Success));

            return RedirectToAction(nameof(Index));
        } 
    }
}
