/**
 * @file IUserRepository.cs
 * @brief Интерфейс для репозитория пользователей.
 */

using AmazingFileVersionControl.Core.Models.UserDbEntities;
using System.Linq.Expressions;

namespace AmazingFileVersionControl.Core.Repositories
{
    /**
     * @interface IUserRepository
     * @brief Интерфейс репозитория для управления пользователями.
     */
    public interface IUserRepository
    {
        /**
         * @brief Добавить нового пользователя.
         * @param user Пользователь.
         */
        Task AddAsync(UserEntity user);

        /**
         * @brief Удалить несколько пользователей по фильтру.
         * @param filter Фильтр для поиска пользователей.
         */
        Task DeleteManyByFilterAsync(Expression<Func<UserEntity, bool>> filter);

        /**
         * @brief Удалить одного пользователя по фильтру.
         * @param filter Фильтр для поиска пользователя.
         */
        Task DeleteOneByFilterAsync(Expression<Func<UserEntity, bool>> filter);

        /**
         * @brief Получить список пользователей по фильтру.
         * @param filter Фильтр для поиска пользователей.
         * @return Список найденных пользователей.
         */
        Task<List<UserEntity>> GetManyByFilterAsync(Expression<Func<UserEntity, bool>> filter);

        /**
         * @brief Получить одного пользователя по фильтру.
         * @param filter Фильтр для поиска пользователя.
         * @return Найденный пользователь.
         */
        Task<UserEntity> GetOneByFilterAsync(Expression<Func<UserEntity, bool>> filter);

        /**
         * @brief Обновить информацию о нескольких пользователях по фильтру.
         * @param filter Фильтр для поиска пользователей.
         * @param updateAction Действие для обновления.
         */
        Task UpdateManyByFilterAsync(Expression<Func<UserEntity, bool>> filter, Action<UserEntity> updateAction);

        /**
         * @brief Обновить информацию об одном пользователе по фильтру.
         * @param filter Фильтр для поиска пользователя.
         * @param updateAction Действие для обновления.
         */
        Task UpdateOneByFilterAsync(Expression<Func<UserEntity, bool>> filter, Action<UserEntity> updateAction);
    }
}
