using System.Configuration;
using Autofac;
using Autofac.Integration.Mvc;
using System.Reflection;
using System.Web.Mvc;
using SocialCRM.Web.Client.Authentication;
using SocialCRM.Web.Client.Services;

namespace SocialCRM.Web.Client
{
    public class AutofacConfig
    {
        public static void Configure()
        {
            var builder = new ContainerBuilder();

            // Regiser MVC controllers
            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            var webApiUrl = ConfigurationManager.AppSettings["WebApiUri"];
            builder.Register(authencticationManager => new AuthenticationManager(webApiUrl));
            builder.Register(clientService => new ClientService(webApiUrl));
            builder.Register(userService => new UserService(webApiUrl));

            var container = builder.Build();

            // Setting the dependency resolver for MVC
            var mvcResolver = new AutofacDependencyResolver(container);
            DependencyResolver.SetResolver(mvcResolver);
        }
    }
}