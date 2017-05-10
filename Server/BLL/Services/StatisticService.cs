using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using DtoLibrary;

namespace BLL.Services
{
    public class StatisticService: IStatisticService
    {
        private IUnitOfWork Database { get; set; }

        public StatisticService(IUnitOfWork database)
        {
            Database = database;
        }
        #region Implementation of ICrud<StatisticDto>

        public void Insert(StatisticDto item)
        {
            ClientProfile clientProfile = Database.ClientManager.GetClientByUserName(item.UserName);
            StatisticEntity statisticEntity = new StatisticEntity()
            {
                Id = item.Id,
                ClientProfileId = clientProfile.Id,
                Illumination = item.Illumination,
                StartTime =  item.StartTime,
                EndTime = item.EndTime,
                ModeId = item.Mode.Id
            };

            Database.StatisticManager.Insert(statisticEntity);
            Database.SaveAsync();
        }

        public void Remove(StatisticDto item)
        {
            Database.StatisticManager.Delete(item.Id);
        }

        #endregion
    }
}
