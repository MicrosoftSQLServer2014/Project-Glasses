using System.Web.Http;
using BLL.Interfaces;
using DtoLibrary;

namespace Server.Controllers
{
    public class ModeController : ApiController
    {
        public IModeService ModeService { get; set; }

        public ModeController(IModeService modeService)
        {
            ModeService = modeService;
        }

        public void AddMode(ModeDto modeDto)
        {
            ModeService.Insert(modeDto);
        }

        public void ChangeMode(ModeDto modeDto)
        {
            ModeService.Insert(modeDto);
        }

        public void RemoveMode(ModeDto modeDto)
        {
            ModeService.Remove(modeDto);
        }

        //private GlassesDto Initialize()
        //{
        //    GlassesDto initGlass = new GlassesDto()
        //    {};
        //}
    }
}
