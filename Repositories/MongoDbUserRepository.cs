using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DeadlineNote.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DeadlineNote.Repositories
{
    public class MongoDbUserRepository : IUserRepository
    {
        private const string databaseName = "dnl-db";

        private const string collectionName = "users";
        private readonly IMongoCollection<User> usersConnection;
        private readonly FilterDefinitionBuilder<User> filterBuilder = Builders<User>.Filter;
        public MongoDbUserRepository(IMongoClient mongoClient)
        {
            IMongoDatabase database = mongoClient.GetDatabase(databaseName);
            usersConnection = database.GetCollection<User>(collectionName);
        }

        public async Task CreateUserAsync(User user)
        {
            await usersConnection.InsertOneAsync(user);
        }

        public async Task DeleteUserAsync(Guid id)
        {
            var filter = filterBuilder.Eq(user => user.id, id);
            await usersConnection.DeleteOneAsync(filter);
        }

        public async Task<User> GetUserAsync(Guid id)
        {
            var filter = filterBuilder.Eq(user => user.id, id);
            return await usersConnection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await usersConnection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task UpdateUserAsync(User user)
        {
            var filter = filterBuilder.Eq(existUser => existUser.id, user.id);
            await usersConnection.ReplaceOneAsync(filter, user);
        }
    }

}