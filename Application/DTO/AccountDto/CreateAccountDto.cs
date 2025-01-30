using System.ComponentModel.DataAnnotations;

namespace Application.Dto
{
    public class CreateAccountDto
    {
        [Required]
        public float Balance { get; set; }

        [Required]
        public Guid UserId { get; set; }
    }
}
