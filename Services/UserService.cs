/**
 * @file UserService.cs
 * @brief Сервис для управления пользователями.
 */

using AmazingFileVersionControl.Core.Infrastructure;
using AmazingFileVersionControl.Core.Models.UserDbEntities;
using AmazingFileVersionControl.Core.Repositories;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AmazingFileVersionControl.Core.Services
{
    /**
     * @class UserService
     * @brief Класс сервиса для управления пользователями.
     */
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IFileService _fileService;
        private readonly IPasswordHasher _passwordHasher;

        /**
         * @brief Конструктор класса UserService.
         * @param userRepository Репозиторий пользователей.
         * @param fileService Сервис для работы с файлами.
         * @param passwordHasher Хэшер паролей.
         */
        public UserService(IUserRepository userRepository,
            IFileService fileService, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _fileService = fileService;
            _passwordHasher = passwordHasher;
        }

        /**
         * @brief Получить пользователя по идентификатору.
         * @param userId Идентификатор пользователя.
         * @return Найденный пользователь.
         */
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

        /**
         * @brief Получить пользователя по логину.
         * @param userLogin Логин пользователя.
         * @return Найденный пользователь.
         */
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

        /**
         * @brief Получить список пользователей по подстроке логина.
         * @param loginSubstring Подстрока логина.
         * @return Список найденных пользователей.
         */
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

        /**
         * @brief Получить список пользователей по подстроке электронной почты.
         * @param emailSubstring Подстрока электронной почты.
         * @return Список найденных пользователей.
         */
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

        /**
         * @brief Получить список всех пользователей.
         * @return Список всех пользователей.
         */
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

        /**
         * @brief Изменить логин пользователя.
         * @param userId Идентификатор пользователя.
         * @param newLogin Новый логин пользователя.
         */
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

        /**
         * @brief Изменить электронную почту пользователя.
         * @param userId Идентификатор пользователя.
         * @param newEmail Новая электронная почта пользователя.
         */
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

        /**
         * @brief Изменить пароль пользователя.
         * @param userId Идентификатор пользователя.
         * @param newPassword Новый пароль пользователя.
         */
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

        /**
         * @brief Изменить роль пользователя.
         * @param userId Идентификатор пользователя.
         * @param newRole Новая роль пользователя.
         */
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

        /**
         * @brief Удалить пользователя по идентификатору.
         * @param userId Идентификатор пользователя.
         */
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
