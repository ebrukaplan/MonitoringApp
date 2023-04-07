using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitoringApp.Model.Entities
{
    public class IntegrationType:BaseEntity
    {
        [Key]
        public int IntegrationTypeId { get; set; }

        [Required]
        [MaxLength(50)]
        public string IntegrationTypeName { get; set; }
    
        [MaxLength(250)]
        public string IntegrationTypeDescription { get; set; }

        public virtual Application Application { get; set; }
    }
}
