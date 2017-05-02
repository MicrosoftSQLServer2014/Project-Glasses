using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using BLL.DTO;
using BLL.Infrastructure;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.AspNet.Identity;

namespace BLL.Services
{
    public class UserService: IUserService
    {
        private IUnitOfWork Database { get; }

        public UserService(IUnitOfWork database)
        {
            Database = database;
        }

        public async Task<OperationDetails> Create(UserDto userDto)
        {
            OperationDetails operationDetails;
            ApplicationUser applicationUser = await Database.UserManager.FindByEmailAsync(userDto.Email);
            if (!ApplicationUser.IsNull(applicationUser))
            {
                operationDetails = new OperationDetails(false, "Change Email", "Email");
            }
            else
            {
                applicationUser = new ApplicationUser() {Email = userDto.Email, UserName = userDto.UserName};
                var result = await Database.UserManager.CreateAsync(applicationUser, userDto.Password);
                operationDetails = OperationDetails.IsAnyError(result);
                if (operationDetails.Succedeed)
                {
                    await Database.UserManager.AddToRoleAsync(applicationUser.Id, userDto.Role);
                    ClientProfile clientProfile =
                        new ClientProfile() {Id = applicationUser.Id, Address = userDto.Address, Name = userDto.Name};
                    Database.ClientManager.Create(clientProfile);
                    await Database.SaveAsync();
                }
            }
            //to do
            return operationDetails;
        }

        public async Task<ClaimsIdentity> Authenticate(UserDto userDto)
        {
            ClaimsIdentity claim = null;
            ApplicationUser user = await Database.UserManager.FindAsync(userDto.Email, userDto.Password);
            if (!ApplicationUser.IsNull(user))
            {
                claim =
                    await Database.UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            }
            return claim;
        }

        public async Task SetInitialData(UserDto userDto, List<string> roles)
        {
            foreach (string roleName in roles)
            {
                var role = await Database.RoleManager.FindByNameAsync(roleName);
                if (role == null)
                {
                    role = new ApplicationRole { Name = roleName };
                    await Database.RoleManager.CreateAsync(role);
                }
            }
            await Create(userDto);
        }

        #region Implementation of IDisposable

        public void Dispose()
        {
            Database.Dispose();
        }

        #endregion
    }
}
