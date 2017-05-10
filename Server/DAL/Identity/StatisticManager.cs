using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DAL.EF;
using DAL.Entities;
using DAL.Interfaces.EntityInterfaces;

namespace DAL.Identity
{
    public class StatisticManager: IStatisticManager
    {

        private ApplicationContext Database { get; }

        public StatisticManager(ApplicationContext database)
        {
            Database = database;
        }

        #region Implementation of ICrud<StatisticEntity>

        public void Insert(StatisticEntity item)
        {
            Database.Entry(item).State = item.Id == "" ? EntityState.Added : EntityState.Modified;
            Database.SaveChanges();
        }

        public StatisticEntity ReadItem(string id)
        {
            return Database.Statistic.FirstOrDefault(item => item.Id == id);
        }

        public ICollection<StatisticEntity> GetElementsByClient(string userName)
        {
            ClientProfile client = Database.ClientProfiles.FirstOrDefault(
                item => item.ApplicationUser.Logins.ToArray()[0].ProviderKey == userName);
            if (client == null)
            {
                return null;
            }
            var statisticEntities = Database.Statistic.Where(item => item.ClientProfileId == client.Id).ToList();
            return statisticEntities;
        }

        public void Delete(string id)
        {
            var statistic = Database.Statistic.FirstOrDefault(item => item.Id == id);
            if (statistic == null) return;
            Database.Entry(statistic).State = EntityState.Deleted;
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
