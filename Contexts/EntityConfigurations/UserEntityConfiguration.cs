/**
 * @file UserEntityConfiguration.cs
 * @brief Конфигурация сущности пользователя для Entity Framework.
 */

using AmazingFileVersionControl.Core.Models.UserDbEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AmazingFileVersionControl.Core.Contexts.EntityConfigurations
{
    /**
     * @class UserEntityConfiguration
     * @brief Класс для конфигурации сущности пользователя.
     */
    public class UserEntityConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        /**
         * @brief Настройка конфигурации сущности пользователя.
         * @param builder Строитель сущности.
         */
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder
               .HasKey(u => u.Id);

            builder
                .HasIndex(u => u.Login)
                .IsUnique();

            builder
                .HasIndex(u => u.Email)
                .IsUnique();

            builder
                .Property(u => u.Login)
                .IsRequired();

            builder
                .Property(u => u.Email)
                .IsRequired();

            builder.Property(u => u.PasswordHash)
                .IsRequired();

            builder
               .Property(u => u.RoleInSystem)
               .IsRequired();

            builder
                .Property(u => u.RoleInSystem)
                .HasConversion(
                    v => v.ToString(),
                    v => (RoleInSystem)Enum.Parse(typeof(RoleInSystem), v));
        }
    }
}
