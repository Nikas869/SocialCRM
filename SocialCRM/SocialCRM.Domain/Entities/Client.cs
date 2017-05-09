using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialCRM.Domain.Entities
{
    public class Client
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ClientId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Country { get; set; }
        public string Town { get; set; }
        public string Email { get; set; }
        public string ShippingAddress { get; set; }
        public string Description { get; set; }
        public ApplicationUser User { get; set; }
        public DateTime CreationDate { get; set; }
    }
}