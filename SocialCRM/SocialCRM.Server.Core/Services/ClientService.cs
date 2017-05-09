using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using SocialCRM.Domain.Entities;
using SocialCRM.Dtos.Models;
using SocialCRM.Server.Core.Interfaces;
using SocialCRM.Server.DataAccess.UnitsOfWork;

namespace SocialCRM.Server.Core.Services
{
    class ClientService : IClientService
    {
        private readonly IUnitOfWork unitOfWork;

        public ClientService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Guid> CreateAsync(CreateClientDto clientDto, string userId)
        {
            var user = await unitOfWork.GetRepository<ApplicationUser>().GetByIdAsync(userId);

            if (user == null)
            {
                throw new ArgumentException();
            }

            var newClient = Mapper.Map<CreateClientDto, Client>(clientDto);
            newClient.User = user;
            newClient.CreationDate = DateTime.UtcNow;

            unitOfWork.GetRepository<Client>().Insert(newClient);
            await this.unitOfWork.SaveAsync();

            return newClient.ClientId;
        }

        public async Task<ClientDto> GetByIdAsync(Guid clientId, string userId)
        {
            var client = await unitOfWork.GetRepository<Client>().GetByIdAsync(clientId);

            if (client == null || client.User.Id != userId)
            {
                return null;
            }

            return Mapper.Map<Client, ClientDto>(client);
        }

        public IEnumerable<ClientDto> Get(string userId)
        {
            var clients = unitOfWork.GetRepository<Client>().Get(c => c.User.Id == userId);

            return Mapper.Map<IList<Client>, IList<ClientDto>>(clients);
        }
    }
}