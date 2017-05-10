using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using BLL.Infrastructure;
using DtoLibrary;
using Microsoft.Owin.Security.OAuth;

namespace BLL.Interfaces
{
    public interface IUserService:IDisposable
    {
        Task<OperationDetails> Create(UserDto userDto);
        Task<ClaimsIdentity> Authenticate(UserDto userDto);
        Task SetInitialData(UserDto userDto, List<string> roles);
        Task<string> GetUserNameAsync(OAuthGrantResourceOwnerCredentialsContext context);
        Task<ClaimsIdentity> GenerateUserIdentityAsync(
            OAuthGrantResourceOwnerCredentialsContext context, string authenticationType);
    }
}
