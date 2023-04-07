using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MonitoringApp.API.IServices;
using MonitoringApp.API.Services;
using MonitoringApp.Model.RequestResponseClasses;

namespace MonitoringApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ApplicationController : ControllerBase
    {

        private readonly IApplicationService _appService;
        public ApplicationController(IApplicationService appService)
        {
            _appService = appService;
        }

        [HttpGet]
        public IActionResult GetApplications()
        {
            var response = _appService.GetApplications();
            return Ok(response);
        }
    }
}
