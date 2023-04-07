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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Role>().HasData(
             new Role {RoleId=1, RoleName = "Standart", CreatedDate = DateTime.Now },
             new Role {RoleId=2, RoleName = "Admin", CreatedDate = DateTime.Now }
             );

            modelBuilder.Entity<User>().HasData(
                new User
                {UserId=1, AccountName = "Ebru", HashPassword = "PuB/hselpQNY0TYpY06RfnYVNjE=", CreatedDate = DateTime.Now, RoleId = 1 },
                new User
                {UserId=2, AccountName = "admin", HashPassword = "PuB/hselpQNY0TYpY06RfnYVNjE=", CreatedDate = DateTime.Now, RoleId = 2 }
                );

            modelBuilder.Entity<IntegrationType>().HasData(
                new IntegrationType {IntegrationTypeId=1, IntegrationTypeName = "email", CreatedDate = DateTime.Now, IntegrationTypeDescription = "when the app has down, send an email" }
                );

            modelBuilder.Entity<Application>().HasData(
                new Application {ApplicationId=1, ApplicationName = "Google", ApplicationUrl = "www.google.com", CreatedDate = DateTime.Now, CreatedBy = 1, IntegrationTypeId = 1, MonitorInterval = 30 }
                );
        }

    }
}
