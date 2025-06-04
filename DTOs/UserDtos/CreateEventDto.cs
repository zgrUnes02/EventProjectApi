using EventProjectApi.Helpers.CustomAttribute;
using System.ComponentModel.DataAnnotations;

namespace EventProjectApi.DTOs.UserDtos
{
    public class CreateEventDto
    {
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        [EventNotInPast(ErrorMessage = "The event date should be in the future")]
        public DateTime EventDate { get; set; }
    }
}
