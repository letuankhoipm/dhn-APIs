using System;
using System.Collections.Generic;
using DeadlineNote.Entities;

namespace DeadlineNote.Repositories
{
    public interface IUserRepository
    {
        User GetUser(Guid id);
        IEnumerable<User> GetUsers();

        void CreateUser(User user);
        void UpdateUser(User user);
        void DeleteUser(Guid id);
    }
}