using frontend_net.API;
using frontend_net.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Reflection;

namespace frontend_net.Controllers
{
    public class SettingsController : Controller
    {
        private readonly IConfiguration _configuration;

        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly Request _request;

        public SettingsController(IConfiguration configuration, IHttpContextAccessor httpContextAccessor, Request request)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _request = request;
        }

        //public IActionResult Settings()
        //{
        //    ViewBag.Token = HttpContext.Session.GetString("token");

        //    return View();
        //}

        [HttpGet]
        public IActionResult Settings()
        {
            User user = new User();
            user.Token = _httpContextAccessor.HttpContext.Session.GetString("token");
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Settings(User user)
        {
            if (ModelState.IsValid)
            {
                Request Api = new Request(_configuration, _httpContextAccessor);
                var result = await Api.UpdateUser(user);

                if (result)
                {
                    return RedirectToAction("Profile","Profile");
                }
            }

            return View(user);
        }
    }
}
