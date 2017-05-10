using System.Web.Http;
using BLL.Interfaces;
using DtoLibrary;

namespace Server.Controllers
{
    [Authorize]
    public class GlassesController : ApiController
    {
        public IGlassesService GlassesService { get; set; }

        public GlassesController(IGlassesService glassesService)
        {
            GlassesService = glassesService;
        }

        public void AddGlasses(GlassesDto glassesDto)
        {
            GlassesService.Insert(glassesDto);    
        }

        public void ChangeGlasses(GlassesDto glassesDto)
        {
            GlassesService.Insert(glassesDto);
        }

        public void RemoveGlasses(GlassesDto glassesDto)
        {
            GlassesService.Remove(glassesDto);
        }
    }
}
