using RestSharp;

namespace MonitoringApp.UI
{
    public static class Globals
    {
        public static RestClient ApiClient { get; private set; } = new RestClient("http://localhost:5096/");
    }
}
