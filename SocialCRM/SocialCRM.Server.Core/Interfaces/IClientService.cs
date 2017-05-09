using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SocialCRM.Dtos.Models;

namespace SocialCRM.Server.Core.Interfaces
{
    public interface IClientService
    {
        Task<Guid> CreateAsync(CreateClientDto clientDto, string userId);
        Task<ClientDto> GetByIdAsync(Guid clientId, string userId);
        IEnumerable<ClientDto> Get(string userId);
    }
}
