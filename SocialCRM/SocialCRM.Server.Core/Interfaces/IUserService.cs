using System.Threading.Tasks;
using SocialCRM.Dtos.Models;

namespace SocialCRM.Server.Core.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> GetByIdAsync(string userId);
        bool IsEmailAvailable(string email);
    }
}