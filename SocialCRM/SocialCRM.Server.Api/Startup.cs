using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;

[assembly: OwinStartup(typeof(SocialCRM.Server.Api.Startup))]

namespace SocialCRM.Server.Api
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);

            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            AutofacConfig.Configure(builder);

            var conatiner = builder.Build();

            var resolver = new AutofacWebApiDependencyResolver(conatiner);
            config.DependencyResolver = resolver;

            app.UseAutofacMiddleware(conatiner);
            app.UseAutofacWebApi(config);
            ConfigureAuth(app);
            app.UseCors(CorsOptions.AllowAll);
            app.UseWebApi(config);
        }
    }
}