using System.ComponentModel.DataAnnotations;

namespace Application.Dto
{
    public class CreateUserAccountResponse
    {
        [Required]
        public float Balance { get; set; }
        [Required]
        public Guid Id { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        [Required]
        public DateTime UpdatedAt { get; set; }
    }
}
