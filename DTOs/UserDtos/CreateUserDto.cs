using System.ComponentModel.DataAnnotations;

namespace EventProjectApi.DTOs.UserDtos
{
    public class CreateUserDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }
    }
}
