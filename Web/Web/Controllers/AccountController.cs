using System.Web.Mvc;
using Newtonsoft.Json.Linq;
using Web.Util;
using DtoLibrary;


namespace Web.Controllers
{
    public class AccountController : Controller
    {
        const bool api = true;
        private const bool notApi = false;

        [HttpPost]
        public JObject Token()
        {           
            var obj = Resender.ReadResponse<JObject>(notApi);
            return obj;
        }

        [HttpGet]
        public ActionResult Register()
        {
            UserDto model = new UserDto();
            return View(model);
        }

        [HttpPost]
        public string Register(UserDto userDto)
        {
            var obj = Resender.ReadResponse<string>(api);
            return obj;
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
    }
}