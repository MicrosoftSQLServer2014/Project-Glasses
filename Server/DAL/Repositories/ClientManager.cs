using DAL.EF;
using DAL.Entities;
using DAL.Interfaces;

namespace DAL.Repositories
{
    class ClientManager : IClientManager
    {
        internal ApplicationContext Database { get; set; }

        public ClientManager(ApplicationContext database)
        {
            Database = database;
        }

        public void Create(ClientProfile item)
        {
            Database.ClientProfiles.Add(item);
            Database.SaveChanges();
        }

        #region Implementation of IDisposable

        public void Dispose()
        {
            Database.Dispose();
        }

        #endregion


    }
}
