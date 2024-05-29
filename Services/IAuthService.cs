/**
 * @file IAuthService.cs
 * @brief Интерфейс для сервиса аутентификации.
 */

namespace AmazingFileVersionControl.Core.Services
{
    /**
     * @interface IAuthService
     * @brief Интерфейс сервиса для аутентификации и регистрации пользователей.
     */
    public interface IAuthService
    {
        /**
         * @brief Вход пользователя в систему.
         * @param login Логин пользователя.
         * @param password Пароль пользователя.
         * @return JWT токен.
         */
        Task<string> LoginAsync(string login, string password);

        /**
         * @brief Регистрация нового пользователя.
         * @param login Логин пользователя.
         * @param email Электронная почта пользователя.
         * @param password Пароль пользователя.
         * @return JWT токен.
         */
        Task<string> RegisterAsync(string login, string email, string password);
    }
}
