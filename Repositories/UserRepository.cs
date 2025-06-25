using EventProjectApi.Database;
using EventProjectApi.DTOs.UserDtos;
using EventProjectApi.Interfaces;
using EventProjectApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace EventProjectApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationContext _context;
        public UserRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<User> CreateUserAsync(CreateUserDto createUserDto)
        {
            try
            {
                User user = new User
                {
                    Name = createUserDto.Name,
                    Email = createUserDto.Email,
                };

                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();

                return user;
            }
            catch (Exception ex) 
            {
                return null;
            }
        }
    }
}
