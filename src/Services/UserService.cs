using MarketplaceBackend.Interfaces;
using MarketplaceBackend.Models;
using MarketplaceBackend.Repositories;
using MongoDB.Bson;

namespace MarketplaceBackend.Services
{
    public class UserService(IUserRepository userRepository)
    {
        public async Task<List<User>> GetAllAsync()
        {
            return await userRepository.GetAllAsync();
        }

        public async Task<User> GetByIdAsync(ObjectId id)
        {
            return await userRepository.GetByIdAsync(id);
        }
        public async Task<User?> GetByAuthTokenAsync(string authToken)
        {
            return await userRepository.GetByAuthTokenAsync(authToken);
        }

        public async Task CreateAsync(User user)
        {
            await userRepository.CreateAsync(user);
        }

        public async Task UpdateAsync(string id, User user)
        {
            await userRepository.UpdateAsync(id, user);
        }

        public async Task DeleteAsync(string id)
        {
            await userRepository.DeleteAsync(id);
        }
    }
}
