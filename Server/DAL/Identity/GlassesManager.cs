using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DAL.EF;
using DAL.Entities;
using DAL.Interfaces.EntityInterfaces;

namespace DAL.Identity
{
    public class GlassesManager : IGlassesManager
    {
        private ApplicationContext Database { get; }

        public GlassesManager(ApplicationContext database)
        {
            Database = database;
        }

        #region Implementation of ICrud<GlassesEntity>

        public void Insert(GlassesEntity item)
        {
            Database.Entry(item).State = item.Id == "" ? EntityState.Added : EntityState.Modified;
            Database.SaveChanges();
        }

        public GlassesEntity ReadItem(string id)
        {
            return Database.Glasses.FirstOrDefault(item => item.Id == id);
        }

        public ICollection<GlassesEntity> GetElementsByClient(string userName)
        {
            var client = Database.ClientProfiles.FirstOrDefault(
                item => item.ApplicationUser.Logins.ToArray()[0].ProviderKey == userName);
            if (client == null)
            {
                return null;
            }
            var glassesEntities = Database.Glasses.Where(item => item.ClientProfileId == client.Id).ToList();
            return glassesEntities;
        }


        public void Delete(string id)
        {
            var glasses = Database.Glasses.FirstOrDefault(item => item.Id == id);
            if (glasses == null) return;
            Database.Entry(glasses).State = EntityState.Deleted;
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
