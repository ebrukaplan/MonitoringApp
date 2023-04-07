using MonitoringApp.Model.RequestResponseClasses;

namespace MonitoringApp.API.IServices
{
    public interface ILoginService
    {
        LoginResponse Login(LoginRequest req);
    }
}
