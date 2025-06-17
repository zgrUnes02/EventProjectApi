using EventProjectApi.Models;
using System.ComponentModel.DataAnnotations;

namespace EventProjectApi.DTOs.EventDtos
{
    public class GetEventsDto
    {
        public string Title { get; set; }

        public string? Description { get; set; } = string.Empty;

        public string Location { get; set; }

        public DateTime EventDate { get; set; }
    }
}
