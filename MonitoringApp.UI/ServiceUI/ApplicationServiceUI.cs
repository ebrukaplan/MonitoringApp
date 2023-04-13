using MonitoringApp.API.IServices;
using MonitoringApp.Model.Entities;
using MonitoringApp.Model.RequestResponseClasses;
using NuGet.Protocol;
using RestSharp;

namespace MonitoringApp.UI.ServiceUI
{
    public class ApplicationServiceUI : IApplicationService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ApplicationServiceUI(IHttpContextAccessor httpContextAccessor)
        {

            _httpContextAccessor = httpContextAccessor;

        }

        public Application? GetApplication(int ApplicationId)
        {
            var qrequest = new RestRequest("api/Application", Method.POST, DataFormat.Json)
           .AddJsonBody(ApplicationId);
            qrequest.AddHeader("Authorization", string.Format("Bearer {0}", _httpContextAccessor.HttpContext.Request.Cookies["UserToken"]));

            var resp = Globals.ApiClient.Execute<Application>(qrequest);
            return resp.Data;
        }

        public List<Application> GetApplications()
        {
            var request = new RestRequest("api/Application/", Method.GET);
            //request.AddHeader("authorization", "Bearer " + request.ToJToken);
            request.AddHeader("Authorization", string.Format("Bearer {0}", _httpContextAccessor.HttpContext.Request.Cookies["UserToken"]));

            var resp = Globals.ApiClient.Execute<List<Application>>(request);
            return resp.Data;
        }

        public bool UpdateApplication(Application app)
        {
            var qrequest = new RestRequest("api/Application", Method.POST, DataFormat.Json)
        .AddJsonBody(app);
            qrequest.AddHeader("Authorization", string.Format("Bearer {0}", _httpContextAccessor.HttpContext.Request.Cookies["UserToken"]));

            var resp = Globals.ApiClient.Execute<bool>(qrequest);
            return resp.Data;
        }
    }
}
