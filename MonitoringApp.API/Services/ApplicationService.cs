using MonitoringApp.API.IServices;
using MonitoringApp.Data;
using MonitoringApp.Model.Entities;

namespace MonitoringApp.API.Services
{
    public class ApplicationService : IApplicationService
    {
        private MonitoringAppDbContext _dbContext;

        public ApplicationService(MonitoringAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Application> GetApplications()
        {
            return _dbContext.Applications.ToList();
        }
    }
}
