using frontend_net.API;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace frontend_net.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IConfiguration _configuration;

        public AuthenticationController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult ValidarSesion(string email, string password)
        {
            Request Api = new Request(_configuration);
            var test = Api.LogIn(email, password);
            HttpContext.Session.SetString("username", test.Username);
            HttpContext.Session.SetString("email", test.Email);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult ValidarRegistro(string username, string email, string password)
        {
            Request Api = new Request(_configuration);
            var test = Api.SignUp(username, email, password);
            HttpContext.Session.SetString("username", test.Username);
            HttpContext.Session.SetString("email", test.Email);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
