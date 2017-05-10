using System.Threading.Tasks;
using System.Web.Http;
using BLL.Infrastructure;
using BLL.Interfaces;
using DtoLibrary;

namespace Server.Controllers
{
    public class AccountController : ApiController
    {
        private  IUserService UserService { get; set; }

        public AccountController(IUserService userService)
        {
            UserService = userService;
        }

        [HttpPost]
        public async Task<string> Register(UserDto userDto)
        {
            string check = userDto.CheckUser();
            if (check == "Ok")
            {
                OperationDetails operationDetails = await UserService.Create(userDto);
                return operationDetails.Message;
            }
            return check;
        }
    }
}