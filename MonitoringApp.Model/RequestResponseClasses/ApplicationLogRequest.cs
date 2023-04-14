using System.ComponentModel.DataAnnotations;

namespace MonitoringApp.API.ReqResponseClasses
{
    public class ApplicationLogRequest
    {
        public int ApplicationId { get; set; }
        
        public string LogMessage { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
