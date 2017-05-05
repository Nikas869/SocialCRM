using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SocialCRM.Server.Core.Interfaces
{
    public interface IRolesService
    {
        IEnumerable<IdentityRole> GetRoles();
    }
}