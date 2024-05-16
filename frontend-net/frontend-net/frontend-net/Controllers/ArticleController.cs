using frontend_net.API;
using frontend_net.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace frontend_net.Controllers
{
    public class ArticleController : Controller
    {
        private readonly Request _request;

        public ArticleController(Request request)
        {
            _request = request;
        }

        public IActionResult Article(string slug)
        {
            var article = _request.GetArticle(slug);
            if (article == null)
            {
                return NotFound();
            }
            return View("Article", article);
        }

        public IActionResult Create_Edit()
        {
            return View();
        }

        public IActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateArticle(string title, string description, string body, List<Tag> tags)
        {
            if (ModelState.IsValid)
            {
                var article = _request.CreateArticle(title, description, body, tags);
                if (article != null)
                {
                    return RedirectToAction("Article", new { slug = article.Slug });
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error creating article.");
                }
            }

            return View("Article");
        }

        [HttpGet]
        public IActionResult UpdateArticle(string slug)
        {
            var article = _request.GetArticle(slug);
            if (article == null)
            {
                return NotFound();
            }
            return View("Edit",article);
        }
        
        [HttpPost]
        public IActionResult UpdateArticle(Article article, string slug)
        {
            if (ModelState.IsValid)
            {
                var updatedArticle = _request.UpdateArticle(article, slug);
                if (updatedArticle != null)
                {
                    return RedirectToAction("Article", new { slug = updatedArticle.Slug });
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error updating article.");
                }
            }
            return View("Edit", article);
        }

        [HttpPost]
        public IActionResult DeleteArticle(string slug)
        {
            var result = _request.DeleteArticle(slug);
            if (result)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Error deleting article.");
                return View();
            }
        }
    }
}
