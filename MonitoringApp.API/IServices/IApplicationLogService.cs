using MonitoringApp.API.ReqResponseClasses;
using MonitoringApp.Model.Entities;

namespace MonitoringApp.API.IServices
{
    public interface IApplicationLogService
    {
        bool AddAppLog(ApplicationLogRequest app);
    }
}
