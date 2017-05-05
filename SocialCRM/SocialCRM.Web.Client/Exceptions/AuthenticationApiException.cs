using System;
using System.Net;

namespace SocialCRM.Web.Client.Exceptions
{
    public class AuthenticationApiException : Exception
    {
        public AuthenticationApiException(HttpStatusCode statusCode, string jsonData)
        {
            StatusCode = statusCode;
            JsonData = jsonData;
        }

        public HttpStatusCode StatusCode { get; }

        public string JsonData { get; }
    }
}