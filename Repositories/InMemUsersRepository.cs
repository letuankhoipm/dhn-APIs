using System;
using System.Collections.Generic;
using System.Linq;
using DeadlineNote.Entities;

namespace DeadlineNote.Repositories
{
    public class InMemUsersRepository : IUserRepository
    {
        private readonly List<User> users = new List<User>()
        {
            new User {id = Guid.NewGuid(), name = "John", createdDate = System.DateTimeOffset.UtcNow},
            new User {id = Guid.NewGuid(), name = "Peter", createdDate = System.DateTimeOffset.UtcNow},
            new User {id = Guid.NewGuid(), name = "Luke", createdDate = System.DateTimeOffset.UtcNow}
        };

        public IEnumerable<User> GetUsers()
        {
            return users;
        }

        public User GetUser(Guid id)
        {
            return users.Where(user => user.id == id).SingleOrDefault();
        }

        public void CreateUser(User user)
        {
            users.Add(user);
        }

        public void UpdateUser(User user)
        {
            var index = users.FindIndex(existUser => existUser.id == user.id);
            users[index] = user;
        }

        public void DeleteUser(Guid id)
        {
            var index = users.FindIndex(existUser => existUser.id == id);
            users.RemoveAt(index);
        }
    }
}