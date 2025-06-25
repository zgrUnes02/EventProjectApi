using EventProjectApi.Database;
using EventProjectApi.DTOs.UserDtos;
using EventProjectApi.Interfaces;
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
        protected readonly IUserRepository _userRepository;
        public UserController(ApplicationContext context, IUserRepository userRepository) 
        { 
            _context = context;
            _userRepository = userRepository;
        }

        [HttpPost]
        [Route("create")]
        public async Task<ActionResult> CreateNewUser(CreateUserDto createUserDto)
        {
            try
            {
                var result = await _userRepository.CreateUserAsync(createUserDto);

                if ( result is not null )
                {
                    return Ok(new
                    {
                        message = "User has been created successfully.",
                        user = result
                    });
                }
                else
                {
                    return BadRequest("Something went wrong while creating new user.");
                }
            }
            catch ( Exception ex )
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
