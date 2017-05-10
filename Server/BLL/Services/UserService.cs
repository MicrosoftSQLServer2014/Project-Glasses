using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using BLL.Infrastructure;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using DtoLibrary;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.OAuth;

namespace BLL.Services
{
    public class UserService: IUserService
    {
        private IUnitOfWork Database { get; }

        public UserService(IUnitOfWork database)
        {
            Database = database;
        }

        public async Task<string> GetUserNameAsync(OAuthGrantResourceOwnerCredentialsContext context)
        {
            ApplicationUser applicationUser = 
                await Database.UserManager.FindAsync(context.UserName, context.Password);
            return applicationUser?.UserName;
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(
            OAuthGrantResourceOwnerCredentialsContext context, string authenticationType)
        {
            ApplicationUser applicationUser =
                await Database.UserManager.FindAsync(context.UserName, context.Password);
            var userIdentity = await Database.UserManager.CreateIdentityAsync(applicationUser, authenticationType);
            return userIdentity;
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
                var role = await Database.RoleManager.FindByNameAsync(userDto.Role);
                if (role == null)
                {
                    role = new ApplicationRole { Name = userDto.Role};
                    await Database.RoleManager.CreateAsync(role);
                    await Database.SaveAsync();
                }
                applicationUser = new ApplicationUser() {Email = userDto.Email, UserName = userDto.UserName};
                var result = await Database.UserManager.CreateAsync(applicationUser, userDto.Password);
                operationDetails = OperationDetails.IsAnyError(result);
                if (operationDetails.Succedeed)
                {

                    await Database.UserManager.AddToRoleAsync(applicationUser.Id, userDto.Role);
                    ClientProfile clientProfile =
                        new ClientProfile() {Id = applicationUser.Id, Age = userDto.Age, Name = userDto.Name};
                    Database.ClientManager.Create(clientProfile);
                    await Database.SaveAsync();
                }
            }
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
