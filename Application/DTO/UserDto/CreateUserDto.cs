using System.ComponentModel.DataAnnotations;

namespace Application.DTO.UserDto
{
    public class CreateUserDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public required string Password { get; set; }
    }
}
