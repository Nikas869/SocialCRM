using System.Collections.Generic;
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
            await this.CheckUniqueness(registerDto.Email, registerDto.Email);
            await this.ValidatePassword(registerDto.Password);

            var user = new ApplicationUser
            {
                Email = registerDto.Email,
                UserName = registerDto.Email,
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName
            };

            IdentityResult result = await this.userManager.CreateAsync(user, registerDto.Password);

            return result;
        }

        public async Task<ApplicationUser> LogInAsync(LoginDto loginDto)
        {
            ApplicationUser user = await this.userManager.FindAsync(loginDto.Email, loginDto.Password);

            return user;
        }

        //public async Task<IdentityResult> ChangePasswordAsync(ChangePasswordDto changePasswordDto)
        //{
        //    IdentityResult result = await this.userManager.ChangePasswordAsync(
        //        changePasswordDto.UserId,
        //        changePasswordDto.OldPassword,
        //        changePasswordDto.NewPassword);

        //    return result;
        //}

        public async Task<IList<string>> GetUserRoles(string userId)
        {
            return await this.userManager.GetRolesAsync(userId);
        }

        private async Task CheckUniqueness(string userName, string email)
        {
            ApplicationUser existingUser = null;
            existingUser = await this.userManager.FindByEmailAsync(email);

            if (existingUser != null)
            {
                throw new EmailAlreadyExistsException();
            }

            existingUser = await this.userManager.FindByNameAsync(userName);

            if (existingUser != null)
            {
                throw new UserNameAlreadyExistsException();
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
