using Microsoft.AspNetCore.Mvc;

namespace frontend_net.Controllers
{
    public class SettingsController : Controller
    {
        public IActionResult Settings()
        {
            ViewBag.Token = HttpContext.Session.GetString("token");

            return View();
        }
    }
}
