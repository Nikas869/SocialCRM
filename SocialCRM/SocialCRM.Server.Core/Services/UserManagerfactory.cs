using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using SocialCRM.Domain.Entities;
using SocialCRM.Server.Core.Interfaces;
using SocialCRM.Server.DataAccess;

namespace SocialCRM.Server.Core.Services
{
    public class UserManagerFactory : IUserManagerFactory
    {
        private readonly ApplicationDbContext context;

        public UserManagerFactory(ApplicationDbContext context)
        {
            this.context = context;
        }

        public virtual UserManager<ApplicationUser> Create(IdentityFactoryOptions<UserManager<ApplicationUser>> options = null)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(this.context));

            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };

            var dataProtectionProvider = options?.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }

            return manager;
        }
    }
}
