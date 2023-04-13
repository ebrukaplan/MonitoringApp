using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitoringApp.Model.Entities
{
    public class ApplicationLog:BaseEntity
    {
        [Key]
        public int LogId { get; set; }

        [Required]
        public int ApplicationId { get; set; }

        [Required]
        [MaxLength(2500)]
        public string LogMessage { get; set; }
        public virtual Application Application { get; set; }
    }
}
