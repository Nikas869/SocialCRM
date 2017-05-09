using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SocialCRM.Dtos.Models;
using SocialCRM.Web.Client.Exceptions;

namespace SocialCRM.Web.Client.Services
{
    public class ClientService : BaseService<ClientDto>
    {
        public ClientService(string baseUri) : base(baseUri)
        {

        }
    }
}