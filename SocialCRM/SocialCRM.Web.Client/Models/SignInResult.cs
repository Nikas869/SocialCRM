using System;
using Newtonsoft.Json;

namespace SocialCRM.Web.Client.Models
{
    public class SignInResult
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        //Included to show all the available properties, but unused in this sample
        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        [JsonProperty("expires_in")]
        public uint ExpiresIn { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("avatar")]
        public string Avatar { get; set; }

        [JsonProperty("id")]
        public string UserId { get; set; }

        [JsonProperty(".issued")]
        public DateTimeOffset Issued { get; set; }

        [JsonProperty(".expires")]
        public DateTimeOffset Expires { get; set; }

    }
}