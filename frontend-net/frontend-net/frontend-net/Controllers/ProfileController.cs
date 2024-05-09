using Microsoft.AspNetCore.Mvc;

namespace frontend_net.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult Profile()
        {
            return View();
        }
    }
}
