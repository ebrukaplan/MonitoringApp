using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitoringApp.Model.Entities
{
    public class User : BaseEntity
    {
        [Key]
        public int UserId { get; set; }

        [Required] 
        [MaxLength(250)]
        public string AccountName { get; set; }

        [Required]
        [MaxLength(50)]
        public string HashPassword { get; set; }

        [Required]
        public int RoleId { get; set; }

        public virtual Role Roles { get; set; }
    }
}
