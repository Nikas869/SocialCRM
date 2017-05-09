using System;
using SocialCRM.Domain.Misc;

namespace SocialCRM.Domain.Entities
{
    public class Lead
    {
        public Guid LeadId { get; set; }
        public string Product { get; set; }
        public decimal Price { get; set; }
        public Client Client { get; set; }
        public DateTime Date { get; set; }
        public LeadStatus Status { get; set; }
        public int Quantity { get; set; }
    }
}