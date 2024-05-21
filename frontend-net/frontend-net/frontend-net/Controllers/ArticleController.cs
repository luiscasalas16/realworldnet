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
            article.Comments = _request.GetComments(slug);
            return View("Article", article);
        }

        public IActionResult Create_Edit()
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
        public IActionResult Edit(string slug)
        {
            var article = _request.GetArticle(slug);
            if (article == null)
            {
                return NotFound();
            }
            return View("Edit", article);
        }

        [HttpPost]
        public IActionResult UpdateArticle(Article article, string slug)
        {
            ModelState.Remove("Author");
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
            return View("Article", article);
        }

        [HttpPost]
        public IActionResult DeleteArticle(string slug)
        {
            var result = _request.DeleteArticle(slug);
            if (result)
            {
                return RedirectToAction("Profile", "Profile");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Error deleting article.");
                return View();
            }
        }

        [HttpPost]
        public IActionResult CreateComment(string slug, string body)
        {
            if (ModelState.IsValid)
            {
                var comment = _request.CreateComment(slug, body);
                if (comment != null)
                {
                    return RedirectToAction("Article", new { slug = slug });
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error creating comment.");
                }
            }

            return View("Article");
        }

        [HttpPost]
        public IActionResult DeleteComment(string slug, int commentId)
        {
            var result = _request.DeleteComment(slug, commentId);
            if (result)
            {
                return RedirectToAction("Article", new { slug = slug });
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Error deleting comment.");
                return View();
            }
        }
    }
}
