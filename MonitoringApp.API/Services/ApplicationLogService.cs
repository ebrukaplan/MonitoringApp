using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MonitoringApp.API.IServices;
using MonitoringApp.API.ReqResponseClasses;
using MonitoringApp.Data;
using MonitoringApp.Model.Entities;

namespace MonitoringApp.API.Services
{
    public class ApplicationLogService : IApplicationLogService
    {
        private MonitoringAppDbContext _dbContext;

        public ApplicationLogService(MonitoringAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public bool AddAppLog(ApplicationLogRequest app)
        {
            ApplicationLog log= new ApplicationLog();
            log.ApplicationId = app.ApplicationId;  
            log.LogMessage = app.LogMessage;
            log.CreatedDate = app.CreatedDate;
            _dbContext.Add(log);
            return _dbContext.SaveChanges() > 0 ? true : false;
        }
    }
}
