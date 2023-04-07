using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitoringApp.Model.Entities
{
    public class BaseEntity
    {

        [Column(TypeName = "datetime2")]
        public DateTime CreatedDate { get; set; }
    }
}
