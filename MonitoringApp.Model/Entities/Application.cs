using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitoringApp.Model.Entities
{
    public class Application:BaseEntity
    {
        [Key]
        public int ApplicationId { get; set; }

        [Required]
        [MaxLength(250)]
        public string ApplicationName { get; set; }

        [Required]
        [MaxLength(1500)]
        public string ApplicationUrl { get; set; }

        [Required]
        public int MonitorInterval { get; set; }

        [Required]
        public int IntegrationTypeId { get; set; }
        public int CreatedBy { get; set; }

        public virtual IntegrationType IntegrationType { get; set; }
    }
}
