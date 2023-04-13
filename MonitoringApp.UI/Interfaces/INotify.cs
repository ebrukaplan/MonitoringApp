namespace MonitoringApp.UI.Interfaces
{
    public interface INotify
    {
        bool NotifyAppStatus(int IntegrationTypeId, string AppName, string NotifyList);
    }
}
