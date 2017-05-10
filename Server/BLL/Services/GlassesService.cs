using DtoLibrary;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;

namespace BLL.Services
{
    public class GlassesService: IGlassesService
    {
        private IUnitOfWork Database { get; set; }

        public GlassesService(IUnitOfWork database)
        {
            Database = database;
        }

        public void Insert(GlassesDto glassesDto)
        {
            ClientProfile clientProfile = Database.ClientManager.GetClientByUserName(glassesDto.UserName);
            GlassesEntity glassesEntity = new GlassesEntity()
            {
                ModeId = glassesDto.ModeId,
                ClientProfileId = clientProfile.Id
            };

            Database.GlassesManager.Insert(glassesEntity);
            Database.SaveAsync();
        }

        public void Remove(GlassesDto glassesDto)
        {
            Database.GlassesManager.Delete(glassesDto.Id);
        }

        public void ChangeMode(GlassesDto glassesDto, ModeDto modeDto)
        {
            ClientProfile clientProfile = Database.ClientManager.GetClientByUserName(glassesDto.UserName);
            GlassesEntity glassesEntity = new GlassesEntity()
            {
                Id = glassesDto.Id,
                ClientProfileId = clientProfile.Id,
                ModeId = modeDto.Id
            };
            Database.GlassesManager.Insert(glassesEntity);
        }
    }
}
