using System;

namespace SocialCRM.Dtos.Models
{
    public class ClientDto
    {
        public Guid ClientId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Country { get; set; }
        public string Town { get; set; }
        public string Email { get; set; }
        public string ShippingAddress { get; set; }
        public string Description { get; set; }
    }

    public class CreateClientDto
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Country { get; set; }
        public string Town { get; set; }
        public string Email { get; set; }
        public string ShippingAddress { get; set; }
        public string Description { get; set; }
    }
}
