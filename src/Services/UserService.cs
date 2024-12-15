using Google.Apis.Auth;
using MarketplaceBackend.Interfaces;
using MarketplaceBackend.Models;
using MarketplaceBackend.Repositories;
using MongoDB.Bson;

namespace MarketplaceBackend.Services
{
    public class UserService(IUserRepository userRepository, ITokenService tokenService)
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly ITokenService _tokenService = tokenService;

        private const int RefreshTokenExpirationDays = 7;

        public async Task<List<User>> GetAllAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task<User?> GetByIdAsync(ObjectId id)
        {
            return await _userRepository.GetByIdAsync(id);
        }
        public async Task<User?> GetByAuthTokenAsync(string authToken)
        {
            return await _userRepository.GetByAuthTokenAsync(authToken);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _userRepository.GetByEmailAsync(email);
        }

        public async Task<User?> GetOrCreateUserByEmailAsync(GoogleJsonWebSignature.Payload payload)
        {
            var user = await _userRepository.GetByEmailAsync(payload.Email);

            if (user != null)
                return user;

            var newUser = new User
            {
                Email = payload.Email,
                SecurityData = new AccountSecurity
                {
                    PasswordHash = null,
                    AuthToken = _tokenService.GenerateAccessToken(payload.Subject,payload.Email),
                    RefreshToken = _tokenService.GenerateRefreshToken(),
                    RefreshTokenExpirationUtc = DateTime.UtcNow.AddDays(RefreshTokenExpirationDays),
                    LastLoginUtc = DateTime.UtcNow
                },
                FirstName = payload.Name,
                LastName = payload.FamilyName,
                CreatedAtUtc = DateTime.UtcNow,
                ProfileData = new ProfileInfo()
                {
                    ProfilePictureUrl = payload.Picture,
                }
            };

            return await _userRepository.CreateAsync(newUser);
        }

        public async Task CreateAsync(User user)
        {
            await _userRepository.CreateAsync(user);
        }

        public async Task UpdateAsync(string id, User user)
        {
            await _userRepository.UpdateAsync(id, user);
        }

        public async Task DeleteAsync(string id)
        {
            await _userRepository.DeleteAsync(id);
        }
    }
}
