﻿/**
 * @file AuthService.cs
 * @brief Сервис для аутентификации пользователей.
 */

using AmazingFileVersionControl.Core.Infrastructure;
using AmazingFileVersionControl.Core.Models.UserDbEntities;
using AmazingFileVersionControl.Core.Repositories;
using System;
using System.Threading.Tasks;

namespace AmazingFileVersionControl.Core.Services
{
    /**
     * @class AuthService
     * @brief Класс сервиса для аутентификации и регистрации пользователей.
     */
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtGenerator _jwtGenerator;
        private readonly IPasswordHasher _passwordHasher;

        /**
         * @brief Конструктор класса AuthService.
         * @param userRepository Репозиторий пользователей.
         * @param jwtGenerator Генератор JWT.
         * @param passwordHasher Хэшер паролей.
         */
        public AuthService(IUserRepository userRepository,
            IJwtGenerator jwtGenerator,
            IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _jwtGenerator = jwtGenerator;
            _passwordHasher = passwordHasher;
        }

        /**
         * @brief Регистрация нового пользователя.
         * @param login Логин пользователя.
         * @param email Электронная почта пользователя.
         * @param password Пароль пользователя.
         * @return JWT токен.
         */
        public async Task<string> RegisterAsync(string login, string email, string password)
        {
            try
            {
                var existingUser = await _userRepository.GetOneByFilterAsync(u => u.Email == email || u.Login == login);

                if (existingUser != null)
                {
                    throw new Exception("User with the same email or login already exists.");
                }

                var hashedPassword = _passwordHasher.HashPassword(password);

                var user = new UserEntity
                {
                    Id = Guid.NewGuid(),
                    Login = login,
                    Email = email,
                    PasswordHash = hashedPassword,
                    RoleInSystem = RoleInSystem.USER,
                };

                await _userRepository.AddAsync(user);

                return _jwtGenerator.GenerateToken(user.Id, user.RoleInSystem.ToString(), user.Login);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while registering the user.", ex);
            }
        }

        /**
         * @brief Вход пользователя в систему.
         * @param login Логин пользователя.
         * @param password Пароль пользователя.
         * @return JWT токен.
         */
        public async Task<string> LoginAsync(string login, string password)
        {
            try
            {
                var user = await _userRepository.GetOneByFilterAsync(u => u.Login == login);

                if (user == null || !_passwordHasher.VerifyPassword(password, user.PasswordHash))
                {
                    throw new Exception("Invalid login credentials.");
                }

                return _jwtGenerator.GenerateToken(user.Id, user.RoleInSystem.ToString(), user.Login);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while logging in.", ex);
            }
        }
    }
}
