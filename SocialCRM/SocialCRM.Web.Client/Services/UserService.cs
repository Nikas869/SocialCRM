using System.Threading.Tasks;
using SocialCRM.Dtos.Models;

namespace SocialCRM.Web.Client.Services
{
    public class UserService : BaseService<UserDto>
    {
        public UserService(string baseUri) : base(baseUri)
        {
        }
    }
}