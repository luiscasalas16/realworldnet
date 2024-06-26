﻿using frontend_net.API;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace frontend_net.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IConfiguration _configuration;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthenticationController(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult ValidarSesion(string email, string password)
        {
            Request Api = new Request(_configuration, _httpContextAccessor);
            var test = Api.LogIn(email, password);
            if(test != null)
            {
                HttpContext.Session.SetString("token", test.Token);
            }
            HttpContext.Session.SetString("username", test.Username);
            HttpContext.Session.SetString("email", test.Email);
            HttpContext.Session.SetString("image", test.Image);
            HttpContext.Session.SetString("bio", test.Bio);           
            return RedirectToAction("Index", "Home");
        }

        public IActionResult ValidarRegistro(string username, string email, string password)
        {
            Request Api = new Request(_configuration, _httpContextAccessor);
            var test = Api.SignUp(username, email, password);
            if (test != null)
            {
                HttpContext.Session.SetString("token", test.Token);
            }
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
