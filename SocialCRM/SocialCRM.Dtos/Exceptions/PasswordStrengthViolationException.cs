using System;

namespace SocialCRM.Dtos.Exceptions
{
    public class PasswordStrengthViolationException : Exception
    {
        public PasswordStrengthViolationException(string message) : base(message)
        {
        }
    }
}
