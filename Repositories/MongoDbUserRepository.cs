using System;
using System.Collections.Generic;
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

        public void CreateUser(User user)
        {
            usersConnection.InsertOne(user);
        }

        public void DeleteUser(Guid id)
        {
            var filter = filterBuilder.Eq(user => user.id, id);
            usersConnection.DeleteOne(filter);
        }

        public User GetUser(Guid id)
        {
            var filter = filterBuilder.Eq(user => user.id, id);
            return usersConnection.Find(filter).SingleOrDefault();
        }

        public IEnumerable<User> GetUsers()
        {
            return usersConnection.Find(new BsonDocument()).ToList();
        }

        public void UpdateUser(User user)
        {
            var filter = filterBuilder.Eq(existUser => existUser.id, user.id);
            usersConnection.ReplaceOne(filter, user);
        }
    }

}