/**
 * @file IUserService.cs
 * @brief Интерфейс для сервиса управления пользователями.
 */

using AmazingFileVersionControl.Core.Models.UserDbEntities;

namespace AmazingFileVersionControl.Core.Services
{
    /**
     * @interface IUserService
     * @brief Интерфейс сервиса для управления пользователями.
     */
    public interface IUserService
    {
        /**
         * @brief Изменить электронную почту пользователя.
         * @param userId Идентификатор пользователя.
         * @param newEmail Новая электронная почта пользователя.
         */
        Task ChangeEmail(string userId, string newEmail);

        /**
         * @brief Изменить логин пользователя.
         * @param userId Идентификатор пользователя.
         * @param newLogin Новый логин пользователя.
         */
        Task ChangeLogin(string userId, string newLogin);

        /**
         * @brief Изменить пароль пользователя.
         * @param userId Идентификатор пользователя.
         * @param newPassword Новый пароль пользователя.
         */
        Task ChangePassword(string userId, string newPassword);

        /**
         * @brief Изменить роль пользователя.
         * @param userId Идентификатор пользователя.
         * @param newRole Новая роль пользователя.
         */
        Task ChangeRole(string userId, RoleInSystem newRole);

        /**
         * @brief Удалить пользователя по идентификатору.
         * @param userId Идентификатор пользователя.
         */
        Task DeleteById(string userId);

        /**
         * @brief Получить список всех пользователей.
         * @return Список всех пользователей.
         */
        Task<List<UserEntity>> GetAll();

        /**
         * @brief Получить список пользователей по подстроке электронной почты.
         * @param emailSubstring Подстрока электронной почты.
         * @return Список найденных пользователей.
         */
        Task<List<UserEntity>> GetAllByEmailSubstring(string emailSubstring);

        /**
         * @brief Получить список пользователей по подстроке логина.
         * @param loginSubstring Подстрока логина.
         * @return Список найденных пользователей.
         */
        Task<List<UserEntity>> GetAllByLoginSubstring(string loginSubstring);

        /**
         * @brief Получить пользователя по идентификатору.
         * @param userId Идентификатор пользователя.
         * @return Найденный пользователь.
         */
        Task<UserEntity> GetById(string userId);

        /**
         * @brief Получить пользователя по логину.
         * @param userLogin Логин пользователя.
         * @return Найденный пользователь.
         */
        Task<UserEntity> GetByLogin(string userLogin);
    }
}
