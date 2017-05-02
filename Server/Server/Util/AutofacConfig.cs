using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using BLL.Infrastructure;
using BLL.Interfaces;
using BLL.Services;

namespace Server.Util
{
    public class AutofacConfig
    {
        public static void ConfigureContainer()
        {
            var builder = ServiceModule.Container("DefaultConnection");
            var config = GlobalConfiguration.Configuration;

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterWebApiFilterProvider(config);

            #region InjectedTypes

            builder.RegisterType<UserService>().As<IUserService>();

            #endregion

            var container = builder.Build();

            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}