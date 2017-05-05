using Autofac;

namespace SocialCRM.Server.Api
{
    public class AutofacConfig
    {
        public static void Configure(ContainerBuilder builder)
        {
            Core.Utilities.AutofacConfig.Configure(builder);
        }
    }
}