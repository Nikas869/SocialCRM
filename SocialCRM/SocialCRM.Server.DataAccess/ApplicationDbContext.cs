using System.Configuration;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using SocialCRM.Domain.Entities;

namespace SocialCRM.Server.DataAccess
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(string connectionName) : base(GetConnectionString(connectionName))
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
            Database.SetInitializer(new ApplicationDbInitializer());
        }

        public DbSet<Client> Clients { get; set; }

        public DbSet<Lead> Leads { get; set; }

        public DbSet<Call> Calls { get; set; }

        public DbSet<UserTask> UserTasks { get; set; }

        private static string GetConnectionString(string connectionName)
        {
            return ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}