using MarketplaceBackend.Data;
using MarketplaceBackend.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading.Tasks;
using MarketplaceBackend.Interfaces;
using MongoDB.Bson;
using System.Text;

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
        public async Task<User?> GetByAuthTokenAsync(string authToken)
        {
            return await _users.Find(user => user.AuthToken == authToken).FirstOrDefaultAsync();
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

        public async Task<User?> ValidateUserCredentialsAsync(string email, string password)
        {
            var user = await _users.Find(u => u.Email == email).FirstOrDefaultAsync();
            if (user == null) return null;

            var hashedPassword = HashPassword(password);
            return user.PasswordHash == hashedPassword ? user : null;
        }

        public async Task<(bool Success, string ErrorMessage, User? User)> RegisterUserAsync(string email, string password, string username)
        {
            var existingUser = await _users.Find(u => u.Email == email).FirstOrDefaultAsync();
            if (existingUser != null)
            {
                return (false, "User with this email already exists.", null);
            }

            var hashedPassword = HashPassword(password);

            var newUser = new User
            {
                Id = ObjectId.GenerateNewId(),
                Email = email,
                PasswordHash = hashedPassword,
                Username = username,
                FirstName = null
            };

            await _users.InsertOneAsync(newUser);
            return (true, string.Empty, newUser);
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}
