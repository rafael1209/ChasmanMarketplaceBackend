using MarketplaceBackend.Models;

namespace MarketplaceBackend.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllAsync();
        Task<User> GetByIdAsync(string id);
        Task CreateAsync(User user);
        Task UpdateAsync(string id, User user);
        Task DeleteAsync(string id);
        Task<User?> ValidateUserCredentialsAsync(string email, string password);
        Task<(bool Success, string ErrorMessage, User User)> RegisterUserAsync(string email, string password, string username);
    }
}
