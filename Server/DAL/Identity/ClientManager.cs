using System;
using System.Linq;
using DAL.EF;
using DAL.Entities;
using DAL.Interfaces;

namespace DAL.Identity
{
    class ClientManager : IClientManager
    {
        private ApplicationContext Database { get; }

        public ClientManager(ApplicationContext database)
        {
            Database = database;
        }

        public void Create(ClientProfile item)
        {
            Database.ClientProfiles.Add(item);
            Database.SaveChanges();
        }

        public ClientProfile GetClientByUserName(string userName)
        {
            ClientProfile clientProfile = Database.ClientProfiles.FirstOrDefault(
                item => item.ApplicationUser.Logins.ToArray()[0].LoginProvider == userName);
            return clientProfile;
        }

        #region Implementation of IDisposable

        public void Dispose()
        {
            Database?.Dispose();
        }

        #endregion


    }
}
