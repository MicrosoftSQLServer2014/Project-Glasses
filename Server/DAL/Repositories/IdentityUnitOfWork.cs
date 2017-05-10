using System;
using System.Threading.Tasks;
using DAL.EF;
using DAL.Entities;
using DAL.Identity;
using DAL.Interfaces;
using DAL.Interfaces.EntityInterfaces;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DAL.Repositories
{
    public class IdentityUnitOfWork : IUnitOfWork
    {
        private ApplicationContext Database { get; }
        private bool Disposed { get; set; }

        public IdentityUnitOfWork(string connectionString)
        {     
            Database = new ApplicationContext(connectionString);
            UserManager = new ApplicationUserManager(new UserStore<ApplicationUser>(Database));
            RoleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(Database));
            ClientManager = new ClientManager(Database);
            GlassesManager = new GlassesManager(Database);
            ModeManager = new ModeManager(Database);
            StatisticManager = new StatisticManager(Database);

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
                ClientManager.Dispose();
                ModeManager.Dispose();
                StatisticManager.Dispose();
                GlassesManager.Dispose();
            }
            Disposed = true;
        }

        #endregion

        #region Implementation of IUnitOfWork

        public ApplicationUserManager UserManager { get; }
        public ApplicationRoleManager RoleManager { get; }
        public IClientManager ClientManager { get; }
        public IGlassesManager GlassesManager { get; }
        public IModeManager ModeManager { get; }
        public IStatisticManager StatisticManager { get; }

        public async Task SaveAsync()
        {
            await Database.SaveChangesAsync();
        }

        #endregion
    }
}
