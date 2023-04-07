using MonitoringApp.API.IServices;
using MonitoringApp.Model.Entities;
using RestSharp;

namespace MonitoringApp.UI.ServiceUI
{
    public class ApplicationServiceUI : IApplicationService
    {
        public List<Application> GetApplications()
        {
            var request = new RestRequest("api/Application/", Method.GET);

            var resp = Globals.ApiClient.Execute<List<Application>>(request);
            return resp.Data;
        }
    }
}
