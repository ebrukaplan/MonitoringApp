using Microsoft.AspNetCore.Mvc;

namespace MonitoringApp.UI.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
