using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using DtoLibrary;

namespace BLL.Services
{
    public class ModeService: IModeService
    {
        private IUnitOfWork Database { get; set; }

        public ModeService(IUnitOfWork database)
        {
            Database = database;
        }

        public void Insert(ModeDto modeDto)
        {
            ClientProfile clientProfile = Database.ClientManager.GetClientByUserName(modeDto.UserName);
            ModeEntity modeEntity = new ModeEntity()
            {
                Id = modeDto.Id,
                ClientProfileId = clientProfile.Id,
                LeftLenseOpticPower = modeDto.LeftLenseOpticPower,
                RightLenseOpticPower = modeDto.RightLenseOpticPower,
                NecessaryIllumination = modeDto.NecessaryIllumination
            };

            Database.ModeManager.Insert(modeEntity);
            Database.SaveAsync();
        }

        public void Remove(ModeDto modeDto)
        {
            Database.ModeManager.Delete(modeDto.Id);
        }
    }
}
