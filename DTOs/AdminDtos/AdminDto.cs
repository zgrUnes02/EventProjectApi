using System.ComponentModel.DataAnnotations;

namespace EventProjectApi.DTOs.AdminDtos
{
    public class AdminDto
    {
        [Required]
        [MinLength(10)]
        [MaxLength(15)]
        public string UserName { get; set; } = string.Empty;

        [Required]
        [MinLength(10)]
        [MaxLength(20)]
        public string Password { get; set; } = string.Empty;
    }
}
