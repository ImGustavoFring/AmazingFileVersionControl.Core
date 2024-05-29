/**
 * @file UserDbContext.cs
 * @brief Контекст базы данных пользователей.
 */

using AmazingFileVersionControl.Core.Contexts.EntityConfigurations;
using AmazingFileVersionControl.Core.Models.UserDbEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AmazingFileVersionControl.Core.Contexts
{
    /**
     * @class UserDbContext
     * @brief Контекст базы данных для сущностей пользователей.
     */
    public class UserDbContext : DbContext
    {
        /** @brief Таблица пользователей. */
        public DbSet<UserEntity> Users { get; set; }

        /**
         * @brief Конструктор без параметров.
         */
        public UserDbContext()
        {
            Database.EnsureCreated();
        }

        /**
         * @brief Конструктор с параметрами конфигурации.
         * @param options Параметры конфигурации DbContext.
         */
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        /**
         * @brief Настройка параметров подключения к базе данных.
         * @param optionsBuilder Строитель параметров DbContext.
         */
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
               .UseLazyLoadingProxies();
        }

        /**
         * @brief Настройка схемы базы данных.
         * @param modelBuilder Строитель модели базы данных.
         */
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
        }
    }
}
