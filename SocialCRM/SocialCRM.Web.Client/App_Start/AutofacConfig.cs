using System.Configuration;
using Autofac;
using Autofac.Integration.Mvc;
using System.Reflection;
using System.Web.Mvc;
using SocialCRM.Web.Client.Authentication;

namespace SocialCRM.Web.Client
{
    public class AutofacConfig
    {
        public static void Configure()
        {
            var builder = new ContainerBuilder();

            // Regiser MVC controllers
            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            builder.Register(authencticationManager => new AuthenticationManager(ConfigurationManager.AppSettings["WebApiUri"]));

            var container = builder.Build();

            // Setting the dependency resolver for MVC
            var mvcResolver = new AutofacDependencyResolver(container);
            DependencyResolver.SetResolver(mvcResolver);
        }
    }
}