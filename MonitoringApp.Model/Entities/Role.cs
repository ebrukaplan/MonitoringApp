using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitoringApp.Model.Entities
{
    public class Role:BaseEntity
    {
        [Key]
        public int RoleId { get; set; }

        [Required]
        [MaxLength(250)]
        public string RoleName { get; set; }
        public virtual List<User> Users { get; set; }
    }
}
