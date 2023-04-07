using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitoringApp.Model.RequestResponseClasses
{
    public class LoginResponse:ResponseBase
    {
        public string UserName { get; set; }
        public string Token { get; set; }
        public int RoleId { get; set; }
    }
}
