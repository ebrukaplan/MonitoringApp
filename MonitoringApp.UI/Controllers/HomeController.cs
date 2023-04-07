using Microsoft.AspNetCore.Mvc;
using MonitoringApp.API.IServices;
using MonitoringApp.Model.Entities;
using MonitoringApp.UI.Models;
using System.Diagnostics;

namespace MonitoringApp.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IApplicationService _appService;

        public HomeController(ILogger<HomeController> logger, IApplicationService appService)
        {
            _logger = logger;
            _appService = appService;
        }

        public IActionResult Index()
        {

           List<Application> appList= _appService.GetApplications();
            return View(appList);
        }

     

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}