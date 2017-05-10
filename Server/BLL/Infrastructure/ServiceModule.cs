using Autofac;
using DAL.Interfaces;
using DAL.Repositories;

namespace BLL.Infrastructure
{
    public class ServiceModule
    {
        public static ContainerBuilder Container(string connectionString)
        {
            ContainerBuilder containerBuilder = new ContainerBuilder();
            containerBuilder.Register(c => new IdentityUnitOfWork(connectionString)).As<IUnitOfWork>();
            return containerBuilder;
        }
    }
}
