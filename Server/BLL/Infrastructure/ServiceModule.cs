using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
