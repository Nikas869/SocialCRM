using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using SocialCRM.Web.Client.Exceptions;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace SocialCRM.Web.Client.Authentication
{
    public class AuthenticationManager
    {
        public AuthenticationManager(string baseUri)
        {
            BaseUri = baseUri;
        }

        public string BaseUri { get; }

        public async Task<T> AuthenticateAsync<T>(string userName, string password)
        {
            using (var client = new HttpClient())
            {
                var result = await client.PostAsync(BuildActionUri("/Token"), new FormUrlEncodedContent(
                    new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("grant_type", "password"),
                        new KeyValuePair<string, string>("userName", userName),
                        new KeyValuePair<string, string>("password", password)
                    }));

                string json = await result.Content.ReadAsStringAsync();
                if (result.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<T>(json);
                }

                throw new AuthenticationApiException(result.StatusCode, json);
            }
        }

        public async Task<T> GetAsync<T>(string action, string authToken = null)
        {
            using (var client = new HttpClient())
            {
                if (!authToken.IsNullOrWhiteSpace())
                {
                    //Add the authorization header
                    client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse("Bearer " + authToken);
                }

                var result = await client.GetAsync(BuildActionUri(action));

                string json = await result.Content.ReadAsStringAsync();
                if (result.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<T>(json);
                }

                throw new AuthenticationApiException(result.StatusCode, json);
            }
        }

        public async Task PutAsync<T>(string action, T data, string authToken = null)
        {
            using (var client = new HttpClient())
            {
                if (!authToken.IsNullOrWhiteSpace())
                {
                    //Add the authorization header
                    client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse("Bearer " + authToken);
                }

                var result = await client.PutAsJsonAsync(BuildActionUri(action), data);
                if (result.IsSuccessStatusCode)
                {
                    return;
                }

                string json = await result.Content.ReadAsStringAsync();
                throw new AuthenticationApiException(result.StatusCode, json);
            }
        }

        public async Task PostAsync<T>(string action, T data, string authToken = null)
        {
            using (var client = new HttpClient())
            {
                if (!authToken.IsNullOrWhiteSpace())
                {
                    //Add the authorization header
                    client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse("Bearer " + authToken);
                }

                var result = await client.PostAsJsonAsync(BuildActionUri(action), data);
                if (result.IsSuccessStatusCode)
                {
                    return;
                }

                string json = await result.Content.ReadAsStringAsync();
                throw new AuthenticationApiException(result.StatusCode, json);
            }
        }

        private string BuildActionUri(string action)
        {
            return BaseUri + action;
        }
    }
}