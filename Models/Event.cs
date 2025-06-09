using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EventProjectApi.Models
{
    public class Event
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; } = string.Empty;

        [Required]
        public string Location { get; set; }

        public DateTime EventDate { get; set; }

        public int OrganizerId { get; set; }
        public User Organizer { get; set; }

        [JsonIgnore]
        public ICollection<Attendee> Attendees { get; set; } = new List<Attendee>();
    }
}
