using Microsoft.Extensions.Caching.Memory;
using MonitoringApp.API.IServices;
using MonitoringApp.API.ReqResponseClasses;
using MonitoringApp.Data;
using MonitoringApp.Model.Entities;
using MonitoringApp.UI.Interfaces;
using Quartz;

namespace MonitoringApp.UI.InterfaceClasses
{
    public class AppControl : IAppControl
    {
        private readonly IApplicationService _appService;
        private readonly IApplicationLogService _appLogService;
        private readonly INotify _notifyService;
        public AppControl(IApplicationService appService, INotify notifyService, IApplicationLogService appLogService)
        {
            _appService = appService;
            _notifyService = notifyService;
            _appLogService = appLogService;

        }
        public Task Execute(IJobExecutionContext context)
        {

            CheckApps();
            return Task.CompletedTask;
            //return Task.FromResult(true);
        }

        private async void CheckApps()
        {
            List<Application> appList = new List<Application>();
            appList = _appService.GetApplications().ToList();
            appList.ForEach(async app =>
            {
                await ControlApps(app);
            }
            );
        }
        private async Task<Task> ControlApps(Application appl)
        {
            return Task.Run(() =>
            {
                var client = new HttpClient();
                while (true)
                {
                    var response = client.GetAsync(appl.ApplicationUrl).Result;

                    if (!response.IsSuccessStatusCode)
                    {
                        var app = _appService.GetApplication(appl.ApplicationId);
                        if (app != null)
                        {
                            ApplicationRequest req = new ApplicationRequest();

                            req.isDown = true;
                            req.isNotified = false;
                            req.ApplicationId = appl.ApplicationId;
                            _appService.UpdateApplication(req);

                            bool isNotified = _notifyService.NotifyAppStatus(appl.IntegrationTypeId, appl.ApplicationName, appl.NotifyList);

                            if (isNotified)
                            {
                                req.isNotified = isNotified;
                                _appService.UpdateApplication(req);
                            }
                        }
                    }
                    ApplicationLogRequest log = new ApplicationLogRequest();
                    log.CreatedDate = DateTime.Now;
                    log.ApplicationId = appl.ApplicationId;
                    log.LogMessage = appl.ApplicationUrl + "- " + response.IsSuccessStatusCode.ToString();
                    _appLogService.AddAppLog(log);
                    //System.Threading.Thread.Sleep(appl.MonitorInterval * 1000);

                }
            });
        }
    }
}
