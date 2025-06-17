using EventProjectApi.DTOs.EventDtos;
using EventProjectApi.Models;
using System.Runtime.CompilerServices;

namespace EventProjectApi.Mappers
{
    public static class EventMappers
    {
        public static GetEventsDto ToGetEventDto(this Event eventModel) 
        {
            return new GetEventsDto
            {
                Title = eventModel.Title,
                Description = eventModel.Description,
                Location = eventModel.Location,
                EventDate = eventModel.EventDate
            };
        }
    }
}
