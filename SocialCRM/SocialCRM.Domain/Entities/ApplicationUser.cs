using System.Security.Claims;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SocialCRM.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ClaimsIdentity GenerateUserIdentity(string authenticationType)
        {
            var userIdentity = new ClaimsIdentity(authenticationType);

            userIdentity.AddClaim(new Claim(ClaimTypes.Name, this.UserName));
            userIdentity.AddClaim(new Claim(ClaimTypes.Email, this.Email));
            userIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, this.Id));

            return userIdentity;
        }
    }
}