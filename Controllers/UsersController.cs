using System;
using System.Collections.Generic;
using System.Linq;
using DeadlineNote.Dtos;
using DeadlineNote.Entities;
using DeadlineNote.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DeadlineNote.Controllers
{
    // GET /users
    [ApiController]
    [Route("user")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository repository;

        public UsersController(IUserRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public IEnumerable<UserDto> GetUsers()
        {
            var users = repository.GetUsers();
            var userTransferred = users.Select(user => user.AsDto());
            return userTransferred;
        }

        [HttpGet("{id}")]
        public ActionResult<UserDto> GetUser(Guid id)
        {
            var user = repository.GetUser(id);

            if (user is null)
            {
                return NotFound();
            }
            return user.AsDto();
        }

        [HttpPost]
        public ActionResult<CreateUserDto> CreateUser(CreateUserDto userDto)
        {
            User user = new()
            {
                id = Guid.NewGuid(),
                name = userDto.name,
                username = userDto.username,
                createdDate = DateTimeOffset.UtcNow
            };

            repository.CreateUser(user);
            return CreatedAtAction(nameof(GetUser), new { id = user.id }, user.AsDto());
        }

        [HttpPut("{id}")]
        public ActionResult<CreateUserDto> UpdateUser(Guid id, UpdateUserDto userDto)
        {
            var existUser = repository.GetUser(id);
            if (existUser is null)
            {
                return NotFound();
            }

            User updatedUser = existUser with
            {
                name = userDto.name,
                username = userDto.username
            };

            repository.UpdateUser(updatedUser);
            return NoContent();

        }

        [HttpDelete("{id}")]
        public ActionResult<Guid> DeleteUser(Guid id)
        {
            var existUser = repository.GetUser(id);

            if (existUser is null)
            {
                return NotFound();
            }

            repository.DeleteUser(id);
            return NoContent();
        }
    }
}