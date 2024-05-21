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

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
