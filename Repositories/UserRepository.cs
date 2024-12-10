using MarketplaceBackend.Data;
using MarketplaceBackend.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace MarketplaceBackend.Repositories
{
    public class UserRepository(MongoDbContext context) : IUserRepository
    {
        private readonly IMongoCollection<User> _users = context.Users;

        public async Task<List<User>> GetAllAsync()
        {
            return await _users.Find(user => true).ToListAsync();
        }

        public async Task<User> GetByIdAsync(string id)
        {
            var userID = ObjectId.Parse(id);

            return await _users.Find(user => user.Id == userID).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            await _users.InsertOneAsync(user);
        }

        public async Task UpdateAsync(string id, User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            var userID = ObjectId.Parse(id);

            var result = await _users.ReplaceOneAsync(u => u.Id == userID, user);

            if (result.ModifiedCount == 0)
            {
                throw new Exception("User not found or no changes made.");
            }
        }

        public async Task DeleteAsync(string id)
        {
            var userID = ObjectId.Parse(id);

            var result = await _users.DeleteOneAsync(user => user.Id == userID);

            if (result.DeletedCount == 0)
            {
                throw new Exception("User not found.");
            }
        }
    }
}
