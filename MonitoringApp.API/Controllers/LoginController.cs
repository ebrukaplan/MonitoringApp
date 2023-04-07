using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MonitoringApp.API.IServices;
using MonitoringApp.Model.RequestResponseClasses;

namespace MonitoringApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILogger<LoginController> _logger;
        private readonly ILoginService _loginService;
        public LoginController(ILogger<LoginController> logger, ILoginService loginService)
        {
            _logger = logger;
            _loginService= loginService;
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login(LoginRequest request)
        {
            var response = _loginService.Login(request);
            if (response.isSuccess == false)
            {
                _logger.LogError(response.ErrorMessage);
                return Unauthorized(response);
            }
            return Ok(response);
        }
    }
}
