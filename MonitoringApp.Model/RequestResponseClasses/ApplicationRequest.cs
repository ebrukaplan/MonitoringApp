using System.ComponentModel.DataAnnotations;

namespace MonitoringApp.API.ReqResponseClasses
{
    public class ApplicationRequest
    {

        public int ApplicationId { get; set; }
        public bool isDown { get; set; }
        public bool isNotified { get; set; }
    }
}
