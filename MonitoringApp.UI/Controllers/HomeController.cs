using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using MonitoringApp.API.IServices;
using MonitoringApp.Model.Entities;
using MonitoringApp.UI.InterfaceClasses;
using MonitoringApp.UI.Interfaces;
using MonitoringApp.UI.Models;
using System.Diagnostics;
using System.Threading;

namespace MonitoringApp.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IApplicationService _appService;
        private readonly INotify _notifyService;
        private readonly IMemoryCache _memoryCache;

        public HomeController(ILogger<HomeController> logger, IApplicationService appService, IMemoryCache memoryCache, INotify notifyService)
        {
            _logger = logger;
            _appService = appService;
            _memoryCache = memoryCache;
            _notifyService = notifyService;
        }

        public async Task<IActionResult> Index()
        {

            SchedulerHelper.SchedulerSetup(1);

            List<Application> appList = GetApplicationListFromCache();

            return View(appList);
        }

        private List<Application> GetApplicationListFromCache()
        {
            List<Application> appList = new List<Application>();
            if (!_memoryCache.TryGetValue("applist", out appList))
            {
                appList = _appService.GetApplications().ToList();
                _memoryCache.Set("applist", appList, DateTimeOffset.Now.AddDays(10));
            }

            return appList;
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}