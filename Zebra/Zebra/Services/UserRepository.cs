using System.Collections.Generic;
using MongoDB.Driver;
using Zebra.Models;

namespace Zebra.Services
{
    public interface IUserRepository
    {
        List<User> Get();
        User Get(string id);
        User Create(User book);
        void Update(string id, User userIn);
        void Remove(User userIn);
        void Remove(string id);
    }

    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _users;

        public UserRepository(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _users = database.GetCollection<User>("Users");
        }

        public List<User> Get() =>
            _users.Find(book => true).ToList();

        public User Get(string id) =>
            _users.Find(book => book.Id == id).FirstOrDefault();

        public User Create(User book)
        {
            _users.InsertOne(book);
            return book;
        }

        public void Update(string id, User bookIn) =>
            _users.ReplaceOne(book => book.Id == id, bookIn);

        public void Remove(User bookIn) =>
            _users.DeleteOne(book => book.Id == bookIn.Id);

        public void Remove(string id) =>
            _users.DeleteOne(book => book.Id == id);
    }
}