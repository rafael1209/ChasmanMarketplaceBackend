using Google.Apis.Auth;
using MarketplaceBackend.Models;
using MarketplaceBackend.Requests;
using MongoDB.Bson;

namespace MarketplaceBackend.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllAsync();
        Task<User> GetByIdAsync(ObjectId id);
        Task<User?> GetByAuthTokenAsync(string authToken);
        Task<User?> GetByEmailAsync(string email);
        Task<User?> CreateAsync(User user);
        Task UpdateAsync(string id, User user);
        Task DeleteAsync(string id);
        Task<User?> ValidateUserCredentialsAsync(string email, string password);
        Task<(bool Success, string ErrorMessage, User User)> RegisterUserAsync(UserRegister regRequest);
    }
}
