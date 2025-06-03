using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventProjectApi.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public ICollection<Event> Events { get; set; } = new List<Event>();

        public ICollection<Attendee> AttendedEvents { get; set; } = new List<Attendee>();

        public User()
        {
            
        }
    }
}
