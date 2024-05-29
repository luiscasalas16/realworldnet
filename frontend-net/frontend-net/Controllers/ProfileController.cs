using frontend_net.API;
using frontend_net.Models;
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

        //public IActionResult Profile()
        //{
        //    var username = HttpContext.Session.GetString("username");
        //    var articles = _request.GetArticlesByUser(username);
        //    return View(articles);
        //}

        public IActionResult Profile(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                username = HttpContext.Session.GetString("username");
            }

            var token = HttpContext.Session.GetString("token");

            var user = _request.GetUser(username, token);
            var articles = _request.GetArticlesByUser(username);
            var isCurrentUser = HttpContext.Session.GetString("username") == username;

            var userProfile = new UserProfile
            {
                User = user,
                Articles = articles,
                IsCurrentUser = isCurrentUser
            };

            return View(userProfile);
        }

        public IActionResult OtherUserProfile(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                username = HttpContext.Session.GetString("username");
            }

            var user = _request.GetOtherUser(username);
            var articles = _request.GetArticlesByUser(username);
            var isCurrentUser = HttpContext.Session.GetString("username") == username;

            var userProfile = new UserProfile
            {
                User = user,
                Articles = articles,
                IsCurrentUser = isCurrentUser
            };

            return View("Profile", userProfile);
        }
    }
}
