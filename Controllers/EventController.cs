using EventProjectApi.Database;
using EventProjectApi.DTOs.UserDtos;
using EventProjectApi.Helpers.Queries;
using EventProjectApi.Mappers;
using EventProjectApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventProjectApi.Controllers
{
    [Route("api/events")]
    [ApiController]
    public class EventController : ControllerBase
    {
        protected readonly ApplicationContext _context;
        public EventController(ApplicationContext context)
        {
            _context = context;
        }

        /// <summary>
        /// API to create new event
        /// </summary>
        /// <param name="createEventDto"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("create")]
        public async Task<ActionResult> CreateNewEvent(CreateEventDto createEventDto, int id)
        {
            try
            {
                if (string.IsNullOrEmpty(id.ToString()) || id == 0)
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
                return BadRequest("Something went wrong while creating new event.");
            }
        }

        /// <summary>
        /// API to add attendee to an event
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="EventId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("{EventId}/attendees")]
        public async Task<ActionResult> AddAttendeeToEvent(int UserId, int EventId)
        {
            try
            {
                if (!String.IsNullOrEmpty(UserId.ToString()))
                {
                    Attendee attendee = new Attendee
                    {
                        UserId = UserId,
                        EventId = EventId,
                        Status = Enums.AttendeStatus.Pending
                    };

                    await _context.Attendees.AddAsync(attendee);
                    await _context.SaveChangesAsync();

                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong while adding attendee to an event.");
            }
        }

        /// <summary>
        /// API to get all events
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> GetAllEvents([FromQuery] AllEventQuery allEventQuery)
        {
            try
            {
                // Filter Event Date ( Past OR Future )
                var query = _context.Events.AsNoTracking();

                if ( !string.IsNullOrEmpty(allEventQuery.eventDate) )
                {
                    string lowerEventDate = allEventQuery.eventDate.ToLower();

                    if ( lowerEventDate == "past" )
                    {
                        query = query.Where(e => e.EventDate < DateTime.Now);
                    }

                    if ( lowerEventDate == "future" )
                    {
                        query = query.Where(e => e.EventDate > DateTime.Now);
                    }
                }

                // Sorting Events By Event Date
                query = allEventQuery.sortingDate?.ToLower() == "asc" 
                    ? query.OrderBy(e => e.EventDate) 
                    : query.OrderByDescending(e => e.EventDate);

                var events = await query
                    .Select(s => s.ToGetEventDto())
                    .Skip((allEventQuery.page - 1) * allEventQuery.pageSize)
                    .Take(allEventQuery.pageSize)
                    .ToListAsync();

                int totalCount = await query.CountAsync();

                return Ok(new
                {
                    events,
                    totalCount
                });
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong while getting all events.");

            }
        }
    }
}
