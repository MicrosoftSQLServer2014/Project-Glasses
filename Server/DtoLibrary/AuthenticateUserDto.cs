using System.ComponentModel.DataAnnotations;
namespace DtoLibrary
{
    public class AuthenticateUserDto
    {
        [Required]
        private string UserName { get; }

        [Required]
        private  string Password { get; }

        public AuthenticateUserDto()
        {
            
        }
        public AuthenticateUserDto(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }
    }
}
