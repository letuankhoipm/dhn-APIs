using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task<IEnumerable<UserDto>> GetUsersAsync()
        {
            var users = await repository.GetUsersAsync();
            var userTransferred = users.Select(user => user.AsDto());
            return userTransferred;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUserAsync(Guid id)
        {
            var user = await repository.GetUserAsync(id);

            if (user is null)
            {
                return NotFound();
            }
            return user.AsDto();
        }

        [HttpPost]
        public async Task<ActionResult<CreateUserDto>> CreateUserAsync(CreateUserDto userDto)
        {
            User user = new()
            {
                id = Guid.NewGuid(),
                name = userDto.name,
                username = userDto.username,
                createdDate = DateTimeOffset.UtcNow
            };

            await repository.CreateUserAsync(user);
            return CreatedAtAction(nameof(GetUserAsync), new { id = user.id }, user.AsDto());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CreateUserDto>> UpdateUserAsync(Guid id, UpdateUserDto userDto)
        {
            var existUser = await repository.GetUserAsync(id);
            if (existUser is null)
            {
                return NotFound();
            }

            User updatedUser = existUser with
            {
                name = userDto.name,
                username = userDto.username
            };

            await repository.UpdateUserAsync(updatedUser);
            return NoContent();

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Guid>> DeleteUserAsync(Guid id)
        {
            var existUser = await repository.GetUserAsync(id);

            if (existUser is null)
            {
                return NotFound();
            }

            await repository.DeleteUserAsync(id);
            return NoContent();
        }
    }
}