using EventProjectApi.Database;
using EventProjectApi.DTOs.UserDtos;
using EventProjectApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventProjectApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        protected readonly ApplicationContext _context;
        public EventController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("create")]
        public async Task<ActionResult> CreateNewEvent(CreateEventDto createEventDto, int id) 
        {
            try
            {
                if ( string.IsNullOrEmpty(id.ToString()) || id == 0 )
                {
                    return BadRequest("Something went wrong while creating new event.");
                }

                Event newEvent = new Event
                {
                    Title = createEventDto.Title,
                    Description = createEventDto.Description,
                    Location = createEventDto.Location,
                    EventDate = createEventDto.EventDate,
                    OrganizerId = id
                };

                await _context.Events.AddAsync(newEvent);
                await _context.SaveChangesAsync();

                return Ok("Event has been created with success.");
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong while creating new user.");
            }
        }
    }
}
