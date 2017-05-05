using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SocialCRM.Server.Core.Interfaces;
using SocialCRM.Server.DataAccess;

namespace SocialCRM.Server.Core.Services
{
    public class RolesService : IRolesService
    {
        private readonly RoleManager<IdentityRole> roleManager;

        public RolesService(ApplicationDbContext context)
        {
            var roleStore = new RoleStore<IdentityRole>(context);
            this.roleManager = new RoleManager<IdentityRole>(roleStore);
        }

        public IEnumerable<IdentityRole> GetRoles()
        {
            var roles = this.roleManager.Roles.ToList();

            return roles;
        }
    }
}