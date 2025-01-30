using Application.Dto;
using System.ComponentModel.DataAnnotations;

namespace Application.DTO.UserDto
{
    public class CreateUserDtoResponse
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public CreateUserAccountResponse Account { get; set; }
    }
}
