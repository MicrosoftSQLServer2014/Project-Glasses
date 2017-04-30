using System;
using System.Threading.Tasks;
using DAL.EF;
using DAL.Entities;
using DAL.Identity;
using DAL.Interfaces;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DAL.Repositories
{
    class IdentityUnitOfWork : IUnitOfWork
    {
        private ApplicationContext Database { get; }
        private bool Disposed { get; set; }

        public IdentityUnitOfWork(string connectionString)
        {
            Database = new ApplicationContext(connectionString);
            UserManager = new ApplicationUserManager(new UserStore<ApplicationUser>(Database));
            RoleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(Database));
            ClientManager = new ClientManager(Database);
            Disposed = false;

        }

        #region Implementation of IDisposable

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                Database.Dispose();
                UserManager.Dispose();
                RoleManager.Dispose();
            }
            Disposed = true;
        }

        #endregion

        #region Implementation of IUnitOfWork

        public ApplicationUserManager UserManager { get; }
        public ApplicationRoleManager RoleManager { get; }
        public IClientManager ClientManager { get; }



        public async Task SaveAsync()
        {
            await Database.SaveChangesAsync();
        }

        #endregion
    }
}
