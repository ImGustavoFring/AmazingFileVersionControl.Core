/**
 * @file UserEntity.cs
 * @brief Описание сущности пользователя.
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazingFileVersionControl.Core.Models.UserDbEntities
{
    /**
     * @class UserEntity
     * @brief Класс, представляющий сущность пользователя.
     */
    public class UserEntity
    {
        /** @brief Идентификатор пользователя. */
        public Guid Id { get; set; }

        /** @brief Логин пользователя. */
        public string Login { get; set; } = null!;

        /** @brief Электронная почта пользователя. */
        public string Email { get; set; } = null!;

        /** @brief Хэш пароля пользователя. */
        public string PasswordHash { get; set; } = null!;

        /** @brief Роль пользователя в системе. */
        public RoleInSystem RoleInSystem { get; set; } = RoleInSystem.USER;

        /** @brief Дата создания пользователя. */
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /** @brief Дата обновления пользователя. */
        public DateTime? UpdatedAt { get; set; } = null;
    }

    /**
     * @enum RoleInSystem
     * @brief Перечисление ролей в системе.
     */
    public enum RoleInSystem
    {
        USER,   /**< Роль пользователя */
        ADMIN   /**< Роль администратора */
    }
}
