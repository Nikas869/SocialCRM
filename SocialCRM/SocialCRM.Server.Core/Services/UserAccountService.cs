using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using SocialCRM.Domain.Entities;
using SocialCRM.Dtos.Exceptions;
using SocialCRM.Dtos.Models;
using SocialCRM.Server.Core.Interfaces;

namespace SocialCRM.Server.Core.Services
{
    public class UserAccountService : IUserAccountService
    {
        private readonly UserManager<ApplicationUser> userManager;

        public UserAccountService(IUserManagerFactory userManagerFactory)
        {
            this.userManager = userManagerFactory.Create();
        }

        public async Task<IdentityResult> RegisterAsync(RegisterDto registerDto)
        {
            await this.CheckUniqueness(registerDto.Email);
            await this.ValidatePassword(registerDto.Password);

            var user = new ApplicationUser
            {
                Email = registerDto.Email,
                UserName = registerDto.Email,
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Avatar = ConfigurationManager.AppSettings["defaultAvatarPath"]
            };

            IdentityResult result = await this.userManager.CreateAsync(user, registerDto.Password);

            return result;
        }

        public async Task<ApplicationUser> LogInAsync(LoginDto loginDto)
        {
            ApplicationUser user = await this.userManager.FindAsync(loginDto.Email, loginDto.Password);

            return user;
        }

        public async Task<IList<string>> GetUserRoles(string userId)
        {
            return await this.userManager.GetRolesAsync(userId);
        }

        private async Task CheckUniqueness(string email)
        {
            ApplicationUser existingUser = null;
            existingUser = await this.userManager.FindByEmailAsync(email);

            if (existingUser != null)
            {
                throw new EmailAlreadyExistsException();
            }
        }

        private async Task ValidatePassword(string password)
        {
            IdentityResult result = await this.userManager.PasswordValidator.ValidateAsync(password);

            if (!result.Succeeded)
            {
                throw new PasswordStrengthViolationException(result.Errors.First());
            }
        }
    }
}
