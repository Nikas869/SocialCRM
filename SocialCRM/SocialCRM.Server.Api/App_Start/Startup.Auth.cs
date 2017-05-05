using System;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using SocialCRM.Server.Api.Providers;

namespace SocialCRM.Server.Api
{
	public partial class Startup
	{
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        public static string PublicClientId { get; private set; }

        public void ConfigureAuth(IAppBuilder app)
        {
	        //BundleTable.EnableOptimizations = false;
			// Configure the application for OAuth based flow
			PublicClientId = "self";
            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/Token"),
                Provider = new ApplicationOAuthProvider(PublicClientId),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
#if DEBUG
                AllowInsecureHttp = true
#endif
            };

            // Enable the application to use bearer tokens to authenticate users
			app.UseOAuthAuthorizationServer(OAuthOptions);
			app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
		}
	}
}