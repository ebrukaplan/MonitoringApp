using Microsoft.AspNetCore.Mvc;
using MonitoringApp.API.IServices;
using MonitoringApp.Model.RequestResponseClasses;

namespace MonitoringApp.UI.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }
        public IActionResult Index()
        {



            return View();
        }

        [HttpPost]
        public IActionResult Index(LoginRequest req)
        {
            var response = _loginService.Login(req);

            if (response == null || response.isSuccess == false || response.Token == null)
            {
                ModelState.AddModelError("", "Bilgiler hatalı");

                return View("Index", req);
            }

            HttpContext.Response.Cookies.Append("UserToken", response.Token);
            HttpContext.Response.Cookies.Append("UserName", response.UserName);

            return RedirectToAction("Index", "Home");
        }
    }
}
