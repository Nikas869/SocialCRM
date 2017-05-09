using System;
using SocialCRM.Domain.Misc;

namespace SocialCRM.Domain.Entities
{
    public class Call
    {
        public Guid CallId { get; set; }
        public Client Client { get; set; }
        public DateTime Date { get; set; }
        public CallStatus Status { get; set; }
        public string Description { get; set; }
    }
}