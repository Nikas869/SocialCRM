using System;
using SocialCRM.Domain.Misc;


namespace SocialCRM.Domain.Entities
{
    public class UserTask
    {
        public Guid UserTaskId { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public UserTaskStatus Status { get; set; }
        public ApplicationUser User { get; set; }
    }
}