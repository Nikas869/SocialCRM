using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using SocialCRM.Domain.Misc;

namespace SocialCRM.Server.DataAccess
{
    public class ApplicationDbInitializer : CreateDatabaseIfNotExists<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            PerformInitialSetup(context);
            base.Seed(context);
        }

        private void PerformInitialSetup(ApplicationDbContext context)
        {
            context.Roles.Add(new IdentityRole(Roles.Customer));
            context.Roles.Add(new IdentityRole(Roles.Store));
        }
    }
}