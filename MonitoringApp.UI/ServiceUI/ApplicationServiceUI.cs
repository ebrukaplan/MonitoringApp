using MonitoringApp.API.IServices;
using MonitoringApp.Model.Entities;
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
        public List<Application> GetApplications()
        {
            var request = new RestRequest("api/Application/", Method.GET);
            //request.AddHeader("authorization", "Bearer " + request.ToJToken);
            request.AddHeader("Authorization", string.Format("Bearer {0}", _httpContextAccessor.HttpContext.Request.Cookies["UserToken"]));

            var resp = Globals.ApiClient.Execute<List<Application>>(request);
            return resp.Data;
        }
    }
}
