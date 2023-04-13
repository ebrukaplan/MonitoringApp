using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using MonitoringApp.API.IServices;
using MonitoringApp.Model.Entities;
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
            List<Application> appList = GetApplicationListFromCache();
            CheckApps(appList);

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

        private async void CheckApps(List<Application> appList)
        {
            foreach (var item in appList)
            {

                await ControlApps(item);
            }
            //var periodicTimeList = appList.Select(a => a.MonitorInterval).Distinct().ToList();

            //foreach (var periodicTime in periodicTimeList)
            //{
            //    var liste = appList.Where(a => a.MonitorInterval == periodicTime).ToList();

            //    await ControlApps(liste, periodicTime);
            //}
        }
        private async Task<Task> ControlApps(Application appl)
        {
            return Task.Run(() =>
            {
                var client = new HttpClient();
                while (true)
                {

                    var response =  client.GetAsync(appl.ApplicationUrl).Result;

                    if (!response.IsSuccessStatusCode)
                    {
                        var app = _appService.GetApplication(appl.ApplicationId);
                        if (app != null)
                        {
                            app.isDown = true;
                            _appService.UpdateApplication(app);

                            bool isNotified = _notifyService.NotifyAppStatus(appl.IntegrationTypeId, appl.ApplicationName, appl.NotifyList);

                            if (isNotified)
                            {
                                app.isNotified = isNotified;
                                _appService.UpdateApplication(app);
                            }
                            List<Application> appList2 = GetApplicationListFromCache();
                            CheckApps(appList2);
                            break;
                        }
                    }
                    System.Threading.Thread.Sleep(appl.MonitorInterval * 1000);

                    //foreach (var item in appList)
                    //{
                    //    var response = await client.GetAsync(item.ApplicationUrl);

                    //    if (!response.IsSuccessStatusCode)
                    //    {
                    //        var app = _appService.GetApplication(item.ApplicationId);
                    //        if (app != null)
                    //        {
                    //            app.isDown = true;
                    //            _appService.UpdateApplication(app);

                    //            bool isNotified = _notifyService.NotifyAppStatus(item.IntegrationTypeId, item.ApplicationName, item.NotifyList);

                    //            if (isNotified)
                    //            {
                    //                app.isNotified = isNotified;
                    //                _appService.UpdateApplication(app);
                    //            }
                    //            List<Application> appList2 = GetApplicationListFromCache();
                    //            CheckApps(appList2);
                    //            break;
                    //        }
                    //    }
                    //    System.Threading.Thread.Sleep(item.MonitorInterval * 1000);
                    //}

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