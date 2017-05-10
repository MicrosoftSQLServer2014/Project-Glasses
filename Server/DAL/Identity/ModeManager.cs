using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DAL.EF;
using DAL.Entities;
using DAL.Interfaces.EntityInterfaces;

namespace DAL.Identity
{
    public class ModeManager: IModeManager
    {
        private ApplicationContext Database { get; }

        public ModeManager(ApplicationContext database)
        {
            Database = database;
        }

        #region Implementation of ICrud<ModeEntity>

        public void Insert(ModeEntity item)
        {
            Database.Entry(item).State = item.Id == "" ? EntityState.Added : EntityState.Modified;
            Database.SaveChanges();
        }

        public ModeEntity ReadItem(string id)
        {
            return Database.Modes.FirstOrDefault(item => item.Id == id);
        }

        public ICollection<ModeEntity> GetElementsByClient(string userName)
        {
            ClientProfile client = Database.ClientProfiles.FirstOrDefault(
                item => item.ApplicationUser.Logins.ToArray()[0].ProviderKey == userName);
            if (client == null)
            {
                return null;
            }
            var modeEntities = Database.Modes.Where(item => item.ClientProfileId == client.Id).ToList();
            return modeEntities;
        }

        public void Delete(string id)
        {
            var mode = Database.Modes.FirstOrDefault(item => item.Id == id);
            if (mode == null) return;
            Database.Entry(mode).State = EntityState.Deleted;
            Database.SaveChanges();
        }

        #endregion

        #region IDisposable

        public void Dispose()
        {
            Database?.Dispose();
        }

        #endregion
    }
}
