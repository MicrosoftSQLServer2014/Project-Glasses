using System.ComponentModel.DataAnnotations;

namespace DtoLibrary
{
    public class UserDto
    {
        public string Id { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public string Role { get; set; }

        public static bool IsNull(UserDto userDto)
        {
            if (userDto == null)
            {
                return true;
            }
            return false;
        }

        public string CheckUser()
        {
            if (UserName == null || Name == null || Password == null ||
                ConfirmPassword == null || Email == null || Age == default(int))
            {
                return "Give all information asked";
            }
            if (ConfirmPassword != Password)
            {
                return "Password doesn't match confirm password";
            }
            if (Role == null)
            {
                Role = "user";
            }
            return "Ok";
        }
    }
}