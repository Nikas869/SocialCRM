using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using SocialCRM.Domain.Entities;

namespace SocialCRM.Server.Core.Interfaces
{
    public interface IUserManagerFactory
    {
        UserManager<ApplicationUser> Create(IdentityFactoryOptions<UserManager<ApplicationUser>> options = null);
    }
}