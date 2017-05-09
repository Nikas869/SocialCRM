using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using SocialCRM.Dtos.Models;
using SocialCRM.Server.Core.Interfaces;

namespace SocialCRM.Server.Api.Controllers
{
    [Authorize]
    public class ClientController : ApiController
    {
        private readonly IClientService clientService;

        public ClientController(IClientService clientService)
        {
            this.clientService = clientService;
        }

        // GET: api/Client
        [Authorize]
        public IEnumerable<ClientDto> Get()
        {
            return clientService.Get(User.Identity.GetUserId());
        }

        // GET: api/Client/5
        public Task<ClientDto> Get(Guid id)
        {
            return clientService.GetByIdAsync(id, User.Identity.GetUserId());
        }

        // POST: api/Client
        public Task<Guid> Post([FromBody]CreateClientDto clientDto)
        {
            if (!ModelState.IsValid)
            {
                throw new ModelValidationException();
            }

            return clientService.CreateAsync(clientDto, User.Identity.GetUserId());
        }

        // PUT: api/Client/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Client/5
        public void Delete(int id)
        {
        }
    }
}
