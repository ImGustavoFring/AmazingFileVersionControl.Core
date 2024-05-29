using AmazingFileVersionControl.Core.Infrastructure;
using AmazingFileVersionControl.Core.Models.UserDbEntities;
using AmazingFileVersionControl.Core.Repositories;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AmazingFileVersionControl.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IFileService _fileService;
        private readonly IPasswordHasher _passwordHasher;

        public UserService(IUserRepository userRepository,
            IFileService fileService, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _fileService = fileService;
            _passwordHasher = passwordHasher;
        }

        public async Task<UserEntity> GetById(string userId)
        {
            try
            {
                var guidUserId = Guid.Parse(userId);
                return await _userRepository.GetOneByFilterAsync(u => u.Id == guidUserId);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to get user by Id.", ex);
            }
        }

        public async Task<UserEntity> GetByLogin(string userLogin)
        {
            try
            {
                return await _userRepository.GetOneByFilterAsync(u => u.Login == userLogin);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to get user by Id.", ex);
            }
        }

        public async Task<List<UserEntity>> GetAllByLoginSubstring(string loginSubstring)
        {
            try
            {
                return await _userRepository.GetManyByFilterAsync(u => u.Login.Contains(loginSubstring));
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to get users by Login substring.", ex);
            }
        }

        public async Task<List<UserEntity>> GetAllByEmailSubstring(string emailSubstring)
        {
            try
            {
                return await _userRepository.GetManyByFilterAsync(u => u.Email.Contains(emailSubstring));
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to get users by Email substring.", ex);
            }
        }

        public async Task<List<UserEntity>> GetAll()
        {
            try
            {
                return await _userRepository.GetManyByFilterAsync(u => true);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to get all users.", ex);
            }
        }

        public async Task ChangeLogin(string userId, string newLogin)
        {
            try
            {
                var guidUserId = Guid.Parse(userId);
                var user = await GetById(userId);
                var oldLogin = user.Login;

                await _userRepository.UpdateOneByFilterAsync(u => u.Id == guidUserId, u =>
                {
                    u.Login = newLogin;
                    u.UpdatedAt = DateTime.UtcNow;
                });

                if (user.Login != oldLogin)
                {
                    await _fileService.UpdateAllFilesInfoAsync(oldLogin, new BsonDocument { { "owner", newLogin } });
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to change user login.", ex);
            }
        }

        public async Task ChangeEmail(string userId, string newEmail)
        {
            try
            {
                var guidUserId = Guid.Parse(userId);
                await _userRepository.UpdateOneByFilterAsync(u => u.Id == guidUserId, u =>
                {
                    u.Email = newEmail;
                    u.UpdatedAt = DateTime.UtcNow;
                });
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to change user email.", ex);
            }
        }

        public async Task ChangePassword(string userId, string newPassword)
        {
            try
            {
                var guidUserId = Guid.Parse(userId);
                string hashNewPassword = _passwordHasher.HashPassword(newPassword);
                await _userRepository.UpdateOneByFilterAsync(u => u.Id == guidUserId, u =>
                {
                    u.PasswordHash = hashNewPassword;
                    u.UpdatedAt = DateTime.UtcNow;
                });
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to change user password.", ex);
            }
        }

        public async Task ChangeRole(string userId, RoleInSystem newRole)
        {
            try
            {
                var guidUserId = Guid.Parse(userId);
                await _userRepository.UpdateOneByFilterAsync(u => u.Id == guidUserId, u =>
                {
                    u.RoleInSystem = newRole;
                    u.UpdatedAt = DateTime.UtcNow;
                });
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to change user role.", ex);
            }
        }

        public async Task DeleteById(string userId)
        {
            try
            {
                var guidUserId = Guid.Parse(userId);
                var user = await GetById(userId);
                await _userRepository.DeleteOneByFilterAsync(u => u.Id == guidUserId);
                await _fileService.DeleteAllFilesAsync(user.Login);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to delete user.", ex);
            }
        }
    }
}
