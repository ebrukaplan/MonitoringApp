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
             new Role { RoleName = "Standart", CreatedDate = DateTime.Now },
             new Role { RoleName = "Admin", CreatedDate = DateTime.Now }
             );

            modelBuilder.Entity<User>().HasData(
                new User
                { AccountName = "Ebru", HashPassword = "123", CreatedDate = DateTime.Now, RoleId = 1 },
                new User
                { AccountName = "admin", HashPassword = "123", CreatedDate = DateTime.Now, RoleId = 2 }
                );

            modelBuilder.Entity<IntegrationType>().HasData(
                new IntegrationType { IntegrationTypeName = "email", CreatedDate = DateTime.Now, IntegrationTypeDescription = "when the app has down, send an email" }
                );

            modelBuilder.Entity<Application>().HasData(
                new Application { ApplicationName = "Google", ApplicationUrl = "www.google.com", CreatedDate = DateTime.Now, CreatedBy = 1, IntegrationTypeId = 1, MonitorInterval = 30 }
                );
        }

    }
}
