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

        public async Task<UserEntity> GetById(Guid userId)
        {
            try
            {
                return await _userRepository.GetOneByFilterAsync(u => u.Id == userId);
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

        public async Task ChangeLogin(Guid userId, string newLogin)
        {
            try
            {
                var user = await GetById(userId);
                var oldLogin = user.Login;

                await _userRepository.UpdateOneByFilterAsync(u => u.Id == userId, u =>
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

        public async Task ChangeEmail(Guid userId, string newEmail)
        {
            try
            {
                await _userRepository.UpdateOneByFilterAsync(u => u.Id == userId, u =>
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

        public async Task ChangePassword(Guid userId, string newPassword)
        {
            try
            {
                string hashNewPassword = _passwordHasher.HashPassword(newPassword);
                await _userRepository.UpdateOneByFilterAsync(u => u.Id == userId, u =>
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

        public async Task ChangeRole(Guid userId, RoleInSystem newRole)
        {
            try
            {
                await _userRepository.UpdateOneByFilterAsync(u => u.Id == userId, u =>
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

        public async Task DeleteById(Guid userId)
        {
            try
            {
                var user = await GetById(userId);
                await _userRepository.DeleteOneByFilterAsync(u => u.Id == userId);
                await _fileService.DeleteAllFilesAsync(user.Login);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to delete user.", ex);
            }
        }
    }
}
