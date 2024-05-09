using frontend_net.API;
using frontend_net.Models;
using Microsoft.AspNetCore.Mvc;
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

        public IActionResult Article()
        {
            return View();
        }

        public IActionResult Create_Edit()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateArticle(Article model)
        {
            if (ModelState.IsValid)
            {
                var article = _request.CreateArticle(model.Title, model.Description, model.Body, model.Tags.ToList());
                if (article != null)
                {
                    return RedirectToAction("Article", new { id = article.Id });
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error creating article.");
                }
            }

            return View("Create_Edit");
        }
    }
}
