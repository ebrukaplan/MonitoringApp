using MonitoringApp.API.IServices;
using MonitoringApp.API.ReqResponseClasses;
using MonitoringApp.Model.Entities;
using RestSharp;

namespace MonitoringApp.UI.ServiceUI
{
    public class ApplicationLogServiceUI : IApplicationLogService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ApplicationLogServiceUI(IHttpContextAccessor httpContextAccessor)
        {

            _httpContextAccessor = httpContextAccessor;

        }
        public bool AddAppLog(ApplicationLogRequest app)
        {
            var qrequest = new RestRequest("api/ApplicationLog", Method.POST, DataFormat.Json)
  .AddJsonBody(app);

            var resp = Globals.ApiClient.Execute<bool>(qrequest);
            return resp.Data;
        }
    }
}
