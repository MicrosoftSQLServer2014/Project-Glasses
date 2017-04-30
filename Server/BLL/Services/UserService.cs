using System;
using System.ComponentModel.Design;
using System.Linq;
using System.Threading.Tasks;
using BLL.DTO;
using BLL.Infrastructure;
using DAL.Entities;
using DAL.Interfaces;

namespace BLL.Services
{
    class UserService
    {
        private IUnitOfWork Database { get; set; }

        public UserService(IUnitOfWork database)
        {
            Database = database;
        }

        public async Task<OperationDetails> Create(UserDto userDto)
        {
            ApplicationUser applicationUser = await Database.UserManager.FindByEmailAsync(userDto.Email);
            if (IsNull(applicationUser))
            {
                return new OperationDetails(false, "Change Email", "Email");
            }   
            applicationUser = new ApplicationUser() { Email = userDto.Email, UserName = userDto.UserName };
            var result = await Database.UserManager.CreateAsync(applicationUser, userDto.Password);
            OperationDetails.IsAnyError(result);
            result = await Database.UserManager.AddToRoleAsync(userDto.Id, userDto.Role);
            ClientProfile clientProfile =
                new ClientProfile() { Id = userDto.Id, Address = userDto.Address, Name = userDto.Name };
            Database.ClientManager.Create(clientProfile);
            await Database.SaveAsync();
            //to do
            return new OperationDetails(true, "OK", "");
        }

        private bool IsNull(ApplicationUser applicationUser)
        {
            throw new NotImplementedException();
        }
    }
}
