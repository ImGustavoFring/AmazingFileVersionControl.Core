using AmazingFileVersionControl.Core.Infrastructure;
using AmazingFileVersionControl.Core.Models.UserDbEntities;
using AmazingFileVersionControl.Core.Repositories;
using System;
using System.Threading.Tasks;

namespace AmazingFileVersionControl.Core.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtGenerator _jwtService;
        private readonly IPasswordHasher _bcCryptService;

        public AuthService(IUserRepository userRepository,
            IJwtGenerator jwtService,
            IPasswordHasher bcCryptService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
            _bcCryptService = bcCryptService;
        }

        public async Task<string> RegisterAsync(string login, string email, string password)
        {
            try
            {
                var existingUser = await _userRepository.GetOneByFilterAsync(u => u.Email == email || u.Login == login);

                if (existingUser != null)
                {
                    throw new Exception("User with the same email or login already exists.");
                }

                var hashedPassword = _bcCryptService.HashPassword(password);

                var user = new UserEntity
                {
                    Id = Guid.NewGuid(),
                    Login = login,
                    Email = email,
                    PasswordHash = hashedPassword,
                    RoleInSystem = RoleInSystem.USER,
                    //Profile = new ProfileEntity()
                };

                //user.Profile.Id = Guid.NewGuid();
                //user.Profile.UserId = user.Id;

                await _userRepository.AddAsync(user);

                return _jwtService.GenerateToken(user.Id, user.RoleInSystem.ToString(), user.Login);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while registering the user.", ex);
            }
        }

        public async Task<string> LoginAsync(string loginOrEmail, string password)
        {
            try
            {
                var user = await _userRepository.GetOneByFilterAsync(u => u.Email == loginOrEmail || u.Login == loginOrEmail);

                if (user == null || !_bcCryptService.VerifyPassword(password, user.PasswordHash))
                {
                    throw new Exception("Invalid login credentials.");
                }

                return _jwtService.GenerateToken(user.Id, user.RoleInSystem.ToString(), user.Login);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while logging in.", ex);
            }
        }
    }
}
