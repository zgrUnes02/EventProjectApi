using EventProjectApi.Database;
using EventProjectApi.DTOs.UserDtos;
using EventProjectApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventProjectApi.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        protected readonly ApplicationContext _context;
        public UserController(ApplicationContext context) 
        { 
            _context = context;
        }

        [HttpPost]
        [Route("create")]
        public async Task<ActionResult> CreateNewUser(CreateUserDto createUserDto)
        {
            try
            {
                User newUser = new User
                {
                    Name = createUserDto.Name,
                    Email = createUserDto.Email,
                };

                await _context.Users.AddAsync(newUser);
                await _context.SaveChangesAsync();

                return Ok("User has been created with success.");
            }
            catch ( Exception ex )
            {
                return BadRequest("Something went wrong while creating new user.");
            }
        }
    }
}
