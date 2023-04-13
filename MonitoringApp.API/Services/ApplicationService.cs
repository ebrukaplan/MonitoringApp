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

        public Application? GetApplication(int ApplicationId)
        {
            return _dbContext.Applications.FirstOrDefault(a => a.ApplicationId == ApplicationId && a.isDown == false);
        }

        public bool UpdateApplication(Application app)
        {
            _dbContext.Update(app);
            return _dbContext.SaveChanges() > 0 ? true : false;
        }
    }
}
