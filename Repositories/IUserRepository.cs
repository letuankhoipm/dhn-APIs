using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DeadlineNote.Entities;

namespace DeadlineNote.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserAsync(Guid id);
        Task<IEnumerable<User>> GetUsersAsync();

        Task CreateUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(Guid id);
    }
}