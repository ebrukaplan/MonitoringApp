using MonitoringApp.Model.Entities;
using MonitoringApp.Model.RequestResponseClasses;

namespace MonitoringApp.API.IServices
{
    public interface IApplicationService
    {
        List<Application> GetApplications();
        Application? GetApplication(int ApplicationId);

        bool UpdateApplication(Application app);


    }
}
