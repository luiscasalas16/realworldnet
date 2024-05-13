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

        public IActionResult Article(int id)
        {
            var article = _request.GetArticle(id);
            if (article == null)
            {
                return NotFound();
            }
            return View(article);
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
                    return RedirectToAction("Article", new { id = article.Id });
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error creating article.");
                }
            }

            return View("Article");
        }
    }
}
