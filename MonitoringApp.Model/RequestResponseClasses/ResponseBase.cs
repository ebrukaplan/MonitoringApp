using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitoringApp.Model.RequestResponseClasses
{
    public class ResponseBase
    {
        public string ErrorMessage { get; set; }
        public bool isSuccess { get; set; }
    }
}
