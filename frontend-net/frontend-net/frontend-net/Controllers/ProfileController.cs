using frontend_net.API;
using Microsoft.AspNetCore.Mvc;

namespace frontend_net.Controllers
{
    public class ProfileController : Controller
    {
        private readonly Request _request;

        public ProfileController(Request request)
        {
            _request = request;
        }

        public IActionResult Profile()
        {
            var username = HttpContext.Session.GetString("username");
            var articles = _request.GetArticlesByUser(username);
            return View(articles);
        }
    }
}
