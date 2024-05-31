using frontend_net.API;
using frontend_net.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;

namespace frontend_net.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IConfiguration _configuration;

        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly Request _request;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration, Request request, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _configuration = configuration;
            _request = request;
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index()
        {
            Request Api = new Request(_configuration, _httpContextAccessor);
            List<Article> articles = Api.GetAllArticles();
            return View(articles);
        }

        public IActionResult YourFeed()
        {
            var token = HttpContext.Session.GetString("token");
            var articles = _request.GetFollowedUsersArticles(token);
            return View("Index", articles);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult AddToFavorites(string slug)
        {
            var article = _request.GetArticle(slug);
            if (article == null)
            {
                return NotFound();
            }

            var username = HttpContext.Session.GetString("username");
            if (username == null)
            {
                return Unauthorized();
            }

            var favorite = new ArticleFavorite
            {
                Username = username,
                ArticleId = article.Id,
                Article = article
            };

            var updatedArticle = _request.AddToFavorite(favorite);
            if (updatedArticle == null)
            {
                ModelState.AddModelError(string.Empty, "Error adding article to favorites.");
                return View("Index", _request.GetAllArticles());
            }

            var articles = _request.GetAllArticles();
            var index = articles.FindIndex(a => a.Slug == updatedArticle.Slug);
            if (index != -1)
            {
                articles[index] = updatedArticle;
            }

            return View("Index", articles);
        }

        //[HttpPost]
        //public IActionResult AddToFavorites(string slug)
        //{
        //    var article = _request.GetArticle(slug);
        //    if (article == null)
        //    {
        //        return NotFound();
        //    }

        //    var username = HttpContext.Session.GetString("username");
        //    if (username == null)
        //    {
        //        return Unauthorized();
        //    }

        //    var favorite = new ArticleFavorite
        //    {
        //        Username = username,
        //        ArticleId = article.Id,
        //        Article = article
        //    };

        //    var updatedArticle = _request.AddToFavorite(favorite);
        //    if (updatedArticle == null)
        //    {
        //        ModelState.AddModelError(string.Empty, "Error adding article to favorites.");
        //        return View("Index", _request.GetAllArticles());
        //    }

        //    return RedirectToAction("Article", "Article", new { slug = updatedArticle.Slug });
        //}
    }
}
