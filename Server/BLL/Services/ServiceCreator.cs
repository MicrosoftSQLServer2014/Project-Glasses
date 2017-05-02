using BLL.Interfaces;
using DAL.Repositories;

namespace BLL.Services
{
    public class ServiceCreator: IServiceCreator
    {
        public IUserService CreateUserService(string connectionString)
        {
            return new UserService(new IdentityUnitOfWork(connectionString));
        }
    }
}
