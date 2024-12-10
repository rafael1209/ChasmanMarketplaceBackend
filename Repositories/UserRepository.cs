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
            return await _users.Find(user => true).ToListAsync(); // Получаем всех пользователей
        }

        // Получить пользователя по ID
        public async Task<User> GetByIdAsync(string id)
        {
            var userID = ObjectId.Parse(id);

            return await _users.Find(user => user.Id == userID).FirstOrDefaultAsync(); // Ищем пользователя по ID
        }

        // Создать нового пользователя
        public async Task CreateAsync(User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user)); // Проверка на null

            // Добавляем пользователя в коллекцию
            await _users.InsertOneAsync(user);
        }

        // Обновить данные пользователя
        public async Task UpdateAsync(string id, User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user)); // Проверка на null

            var userID = ObjectId.Parse(id);

            var result = await _users.ReplaceOneAsync(u => u.Id == userID, user);

            if (result.ModifiedCount == 0)
            {
                throw new Exception("User not found or no changes made.");
            }
        }

        // Удалить пользователя по ID
        public async Task DeleteAsync(string id)
        {
            var userID = ObjectId.Parse(id);

            var result = await _users.DeleteOneAsync(user => user.Id == userID); // Удаляем пользователя по ID

            if (result.DeletedCount == 0)
            {
                throw new Exception("User not found.");
            }
        }
    }
}
