using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MonitoringApp.API.IServices;
using MonitoringApp.API.ReqResponseClasses;
using MonitoringApp.Model.Entities;

namespace MonitoringApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationLogController : ControllerBase
    {
        private readonly IApplicationLogService _appService;
        public ApplicationLogController(IApplicationLogService appService)
        {
            _appService = appService;
        }
        [AllowAnonymous]
        [HttpPost]
        public bool AddAppLog(ApplicationLogRequest app)
        {
            var response = _appService.AddAppLog(app);
            return true;
        }
    }
}
