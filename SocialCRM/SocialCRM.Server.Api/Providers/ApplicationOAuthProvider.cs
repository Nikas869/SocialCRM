using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Autofac;
using Autofac.Integration.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using SocialCRM.Domain.Entities;
using SocialCRM.Dtos.Exceptions;
using SocialCRM.Dtos.Models;
using SocialCRM.Server.Core.Interfaces;

namespace SocialCRM.Server.Api.Providers
{
    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {
        private readonly string publicClientId;

        public ApplicationOAuthProvider(string publicClientId)
        {
            this.publicClientId = publicClientId ?? throw new ArgumentNullException(nameof(publicClientId));
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            ILifetimeScope lifeTimeScope = context.OwinContext.GetAutofacLifetimeScope();
            var userAccountService = lifeTimeScope.Resolve<IUserAccountService>();

            ApplicationUser user = await userAccountService.LogInAsync(new LoginDto(context.UserName, context.Password));

            if (user == null)
            {
                throw new BearerAuthenticationException();
            }

            // Create identity
            ClaimsIdentity oAuthIdentity = user.GenerateUserIdentity(OAuthDefaults.AuthenticationType);

            AuthenticationProperties properties = CreateProperties(user);
            AuthenticationTicket ticket = new AuthenticationTicket(oAuthIdentity, properties);

            // Token generation
            context.Validated(ticket);
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            // Resource owner password credentials does not provide a client ID.
            if (context.ClientId == null)
            {
                context.Validated();
            }

            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        {
            if (context.ClientId == this.publicClientId)
            {
                Uri expectedRootUri = new Uri(context.Request.Uri, "/");

                if (expectedRootUri.AbsoluteUri == context.RedirectUri)
                {
                    context.Validated();
                }
            }

            return Task.FromResult<object>(null);
        }

        public static AuthenticationProperties CreateProperties(ApplicationUser user)
        {
            IDictionary<string, string> data = new Dictionary<string, string>
            {
                { "email", user.Email },
                { "first_name", user.FirstName },
                { "last_name", user.LastName },
                { "avatar", user.Avatar },
                { "id", user.Id }
            };

            return new AuthenticationProperties(data);
        }
    }
}