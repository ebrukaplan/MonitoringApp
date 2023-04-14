using Microsoft.EntityFrameworkCore;
using MonitoringApp.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitoringApp.Data
{
    public class MonitoringAppDbContext : DbContext
    {
        public MonitoringAppDbContext(DbContextOptions<MonitoringAppDbContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Application> Applications { get; set; }
        public DbSet<IntegrationType> IntegrationTypes { get; set; }
        public DbSet<ApplicationLog> ApplicationLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Role>().HasData(
             new Role { RoleId = 1, RoleName = "Standart", CreatedDate = DateTime.Now },
             new Role { RoleId = 2, RoleName = "Admin", CreatedDate = DateTime.Now }
             );

            modelBuilder.Entity<User>().HasData(
                new User
                { UserId = 1, AccountName = "Ebru", HashPassword = "PuB/hselpQNY0TYpY06RfnYVNjE=", CreatedDate = DateTime.Now, RoleId = 1 },
                new User
                { UserId = 2, AccountName = "admin", HashPassword = "PuB/hselpQNY0TYpY06RfnYVNjE=", CreatedDate = DateTime.Now, RoleId = 2 }
                );

            modelBuilder.Entity<IntegrationType>().HasData(
                new IntegrationType { IntegrationTypeId = 1, IntegrationTypeName = "email", CreatedDate = DateTime.Now, IntegrationTypeDescription = "when the app has down, send an email to notifyList" }
                );

            modelBuilder.Entity<Application>().HasData(
                new Application { ApplicationId = 1, ApplicationName = "Google", ApplicationUrl = "https://www.google.com/", CreatedDate = DateTime.Now, CreatedBy = 1, IntegrationTypeId = 1,isDown=false, isNotified=false, NotifyList="", MonitorInterval = 30 },
                new Application { ApplicationId = 2, ApplicationName = "İşbank", ApplicationUrl = "https://www.isbank.com.tr/", CreatedDate = DateTime.Now, CreatedBy = 1, IntegrationTypeId = 1, isDown = false, isNotified = false, NotifyList = "", MonitorInterval = 60 },
                new Application { ApplicationId = 3, ApplicationName = "Edevlet", ApplicationUrl = "https://www.turkiye.gov.tr/", CreatedDate = DateTime.Now, CreatedBy = 1, IntegrationTypeId = 1, isDown = false, isNotified = false, NotifyList = "", MonitorInterval = 60 },
                new Application { ApplicationId = 4, ApplicationName = "Garanti Bankası", ApplicationUrl = "https://www.garantibbva.com.tr/", CreatedDate = DateTime.Now, CreatedBy = 1, IntegrationTypeId = 1, isDown = false, isNotified = false, NotifyList = "", MonitorInterval = 120 },
                new Application { ApplicationId = 5, ApplicationName = "StackOverFlow", ApplicationUrl = "https://stackoverflow.co/explore-teams/?utm_source=adwords&utm_medium=ppc&utm_campaign=kb_teams_search_nb_dsa_targeted_audiences_emea-dach&_bt=646019453177&_bk=&_bm=&_bn=g&gclid=EAIaIQobChMI46KLhaGh_gIVjplRCh18BQVdEAAYASAAEgLIA_D_BwE", CreatedDate = DateTime.Now, CreatedBy = 1, IntegrationTypeId = 1, isDown = false, isNotified = false, NotifyList = "", MonitorInterval = 20 }
                );
        }

    }
}
