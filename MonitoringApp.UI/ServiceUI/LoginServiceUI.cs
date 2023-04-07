using MonitoringApp.API.IServices;
using MonitoringApp.Model.RequestResponseClasses;
using RestSharp;

namespace MonitoringApp.UI.ServiceUI
{
    public class LoginServiceUI : ILoginService
    {
        public LoginResponse Login(LoginRequest req)
        {
            var qrequest = new RestRequest("api/Login", Method.POST, DataFormat.Json)
           .AddJsonBody(req);

            var resp = Globals.ApiClient.Execute<LoginResponse>(qrequest);
            return resp.Data;
        }
    }
}
