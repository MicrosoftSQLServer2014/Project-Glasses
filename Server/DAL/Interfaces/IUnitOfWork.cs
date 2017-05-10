using System;
using System.Threading.Tasks;
using DAL.Identity;
using DAL.Interfaces.EntityInterfaces;

namespace DAL.Interfaces
{
    public interface IUnitOfWork: IDisposable
    {
        ApplicationUserManager UserManager { get; }
        ApplicationRoleManager RoleManager { get; }

        IClientManager ClientManager { get; }
        IGlassesManager GlassesManager { get; }
        IModeManager ModeManager { get; }
        IStatisticManager StatisticManager { get; }

        /// <summary>
        /// Save database async
        /// </summary>
        /// <returns></returns>
        Task SaveAsync();
    }
}
