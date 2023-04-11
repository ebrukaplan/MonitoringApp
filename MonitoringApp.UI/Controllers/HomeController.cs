using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using MonitoringApp.API.IServices;
using MonitoringApp.Model.Entities;
using MonitoringApp.UI.Models;
using System.Diagnostics;
using System.Threading;

namespace MonitoringApp.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IApplicationService _appService;
        private readonly IMemoryCache _memoryCache;

        public HomeController(ILogger<HomeController> logger, IApplicationService appService, IMemoryCache memoryCache)
        {
            _logger = logger;
            _appService = appService;
            _memoryCache = memoryCache;
        }

        public async Task<IActionResult> Index()
        {

            List<Application> appList = new List<Application>();
            if (!_memoryCache.TryGetValue("applist", out appList))
            {
                appList = _appService.GetApplications().ToList();

                _memoryCache.Set("applist", appList, DateTimeOffset.Now.AddDays(10));
            }
            CheckApps(appList);

            return View(appList);
        }

        private async void CheckApps(List<Application> appList)
        {

            var periodicTimeList = appList.Select(a => a.MonitorInterval).Distinct().ToList();

            foreach (var periodicTime in periodicTimeList)
            {
                var liste = appList.Where(a => a.MonitorInterval == periodicTime).ToList();

               await ControlApps(liste, periodicTime);
            }
        }
        private async Task<Task> ControlApps(List<Application> appList, int monitorInterval)
        {
            return Task.Run(() =>
            {
                var client = new HttpClient();
                while (true)
                {
                    foreach (var item in appList)
                    {
                        var response = client.GetAsync(item.ApplicationUrl).Result;

                        if (!response.IsSuccessStatusCode)
                        {
                            //mail at
                            //logla
                        }
                    }
                    System.Threading.Thread.Sleep(monitorInterval * 1000);
                }
            });

        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}