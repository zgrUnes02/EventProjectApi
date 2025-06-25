using System.ComponentModel.DataAnnotations;

namespace EventProjectApi.DTOs.UserDtos
{
    public class CreateUserDto
    {
        [Required]
        [MaxLength(10, ErrorMessage = "The name must be less than 10 characters")]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }
    }
}
