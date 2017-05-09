using AutoMapper;
using SocialCRM.Domain.Entities;
using SocialCRM.Dtos.Models;

namespace SocialCRM.Server.Core.Utilities
{
    public class AutoMapperConfig
    {
        public static void Configure()
        {
            Mapper.Initialize(config =>
            {
                config.CreateMap<CreateClientDto, Client>();
                config.CreateMap<Client, ClientDto>();
                config.CreateMap<ApplicationUser, UserDto>();
            });
        }
    }
}