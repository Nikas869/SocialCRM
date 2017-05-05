using System.Threading.Tasks;
using SocialCRM.Domain.Entities;

namespace SocialCRM.Server.Core.Interfaces
{
    public interface IUserService
    {
        Task<ApplicationUser> GetByIdAsync(string userId);
        bool IsEmailAvailable(string email);
    }
}