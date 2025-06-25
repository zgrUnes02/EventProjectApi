using EventProjectApi.DTOs.UserDtos;
using EventProjectApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace EventProjectApi.Interfaces
{
    public interface IUserRepository
    {
        Task<User> CreateUserAsync(CreateUserDto createUserDto);
    }
}
