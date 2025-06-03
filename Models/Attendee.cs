using EventProjectApi.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventProjectApi.Models
{
    public class Attendee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int EventId { get; set; }
        public Event Event { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public AttendeStatus Status { get; set; } = AttendeStatus.Pending;

    }
}
