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

        [HttpGet]
        public IActionResult Settings()
        {
            string token = HttpContext.Session.GetString("token");
            User user = _request.GetUser(token);
            return View(user);
        }

        [HttpPost]
        public IActionResult UpdateSettings(User user, string token)
        {
            if (ModelState.IsValid)
            {
                Request Api = new Request(_configuration, _httpContextAccessor);
                var result = Api.UpdateUser(user);

                if (result != null)
                {
                    return RedirectToAction("Profile","Profile");
                }
            }

            return View(user);
        }
    }
}
