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
            var builder = new ContainerBuilder();

            // Register Web Api
            WebApiConfig.Register(config);
            // Regtister Web Api Controllers
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            // Autofac
            AutofacConfig.Configure(builder);
            // AutoMapper
            AutoMapperConfig.Configure();

            var conatiner = builder.Build();
            // Dependency resolver
            var resolver = new AutofacWebApiDependencyResolver(conatiner);
            config.DependencyResolver = resolver;
            // Configuring app
            app.UseAutofacMiddleware(conatiner);
            app.UseAutofacWebApi(config);
            ConfigureAuth(app);
            app.UseCors(CorsOptions.AllowAll);
            app.UseWebApi(config);
        }
    }
}