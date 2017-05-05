using System;
using System.Linq;
using System.Threading.Tasks;
using SocialCRM.Domain.Entities;
using SocialCRM.Server.Core.Interfaces;
using SocialCRM.Server.DataAccess.UnitsOfWork;

namespace SocialCRM.Server.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<ApplicationUser> GetByIdAsync(string userId)
        {
            return await this.unitOfWork
                .GetRepository<ApplicationUser>()
                .GetByIdAsync(userId);
        }

        public bool IsEmailAvailable(string email)
        {
            var user = this.unitOfWork.GetRepository<ApplicationUser>()
                .Get(x => x.Email.Equals(email, StringComparison.InvariantCultureIgnoreCase))
                .SingleOrDefault();

            return user == null;
        }
    }
}