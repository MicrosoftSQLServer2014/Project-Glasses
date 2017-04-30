using System;
using System.Threading.Tasks;
using DAL.Identity;

namespace DAL.Interfaces
{
    interface IUnitOfWork: IDisposable
    {
        ApplicationUserManager UserManager { get; }
        ApplicationRoleManager RoleManager { get; }

        IClientManager ClientManager { get; }

        /// <summary>
        /// Save database async
        /// </summary>
        /// <returns></returns>
        Task SaveAsync();
    }
}
