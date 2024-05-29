/**
 * @file LoggingService.cs
 * @brief Сервис для управления логами.
 */

using AmazingFileVersionControl.Core.Models.LoggingEntities;
using AmazingFileVersionControl.Core.Repositories;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazingFileVersionControl.Core.Services
{
    /**
     * @class LoggingService
     * @brief Класс сервиса для управления логами.
     */
    public class LoggingService : ILoggingService
    {
        private readonly ILoggingRepository _loggingRepository;

        /**
         * @brief Конструктор класса LoggingService.
         * @param loggingRepository Репозиторий логов.
         */
        public LoggingService(ILoggingRepository loggingRepository)
        {
            _loggingRepository = loggingRepository;
        }

        /**
         * @brief Логирование события.
         * @param controller Имя контроллера.
         * @param action Имя действия.
         * @param message Сообщение лога.
         * @param level Уровень логирования.
         * @param additionalData Дополнительные данные.
         */
        public async Task LogAsync(string controller,
            string action, string message,
            string level = "Info",
            BsonDocument additionalData = null)
        {
            try
            {
                var logEntry = new LogEntity
                {
                    Controller = controller,
                    Action = action,
                    Message = message,
                    Level = level,
                    AdditionalData = additionalData
                };
                await _loggingRepository.InsertLogAsync(logEntry);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to log the entry.", ex);
            }
        }

        /**
         * @brief Получить лог по идентификатору.
         * @param id Идентификатор лога.
         * @return Найденная запись лога.
         */
        public async Task<LogEntity> GetLogByIdAsync(string id)
        {
            var filter = Builders<LogEntity>.Filter.Eq(log => log.Id, id);
            return await _loggingRepository.GetLogAsync(filter);
        }

        /**
         * @brief Получить список логов по фильтрам.
         * @param controller Имя контроллера.
         * @param action Имя действия.
         * @param startDate Дата начала фильтрации.
         * @param endDate Дата окончания фильтрации.
         * @param level Уровень логирования.
         * @param additionalData Дополнительные данные.
         * @return Список найденных логов.
         */
        public async Task<List<LogEntity>> GetLogsAsync(string controller = null,
            string action = null,
            DateTime? startDate = null, DateTime? endDate = null,
            string level = null,
            BsonDocument additionalData = null)
        {
            var builder = Builders<LogEntity>.Filter;
            var filter = builder.Empty;

            if (!string.IsNullOrEmpty(controller))
            {
                filter &= builder.Eq(log => log.Controller, controller);
            }

            if (!string.IsNullOrEmpty(action))
            {
                filter &= builder.Eq(log => log.Action, action);
            }

            if (startDate.HasValue)
            {
                filter &= builder.Gte(log => log.Timestamp, startDate.Value);
            }

            if (endDate.HasValue)
            {
                filter &= builder.Lte(log => log.Timestamp, endDate.Value);
            }

            if (!string.IsNullOrEmpty(level))
            {
                filter &= builder.Eq(log => log.Level, level);
            }

            if (additionalData != null)
            {
                filter &= builder.Eq(log => log.AdditionalData, additionalData);
            }

            return await _loggingRepository.GetLogsAsync(filter);
        }

        /**
         * @brief Удалить лог по идентификатору.
         * @param id Идентификатор лога.
         */
        public async Task DeleteLogByIdAsync(string id)
        {
            var filter = Builders<LogEntity>.Filter.Eq(log => log.Id, id);
            await _loggingRepository.DeleteLogAsync(filter);
        }

        /**
         * @brief Удалить список логов по фильтрам.
         * @param controller Имя контроллера.
         * @param action Имя действия.
         * @param startDate Дата начала фильтрации.
         * @param endDate Дата окончания фильтрации.
         * @param level Уровень логирования.
         * @param additionalData Дополнительные данные.
         */
        public async Task DeleteLogsAsync(string controller = null,
            string action = null,
            DateTime? startDate = null, DateTime? endDate = null,
            string level = null,
            BsonDocument additionalData = null)
        {
            var builder = Builders<LogEntity>.Filter;
            var filter = builder.Empty;

            if (!string.IsNullOrEmpty(controller))
            {
                filter &= builder.Eq(log => log.Controller, controller);
            }

            if (!string.IsNullOrEmpty(action))
            {
                filter &= builder.Eq(log => log.Action, action);
            }

            if (startDate.HasValue)
            {
                filter &= builder.Gte(log => log.Timestamp, startDate.Value);
            }

            if (endDate.HasValue)
            {
                filter &= builder.Lte(log => log.Timestamp, endDate.Value);
            }

            if (!string.IsNullOrEmpty(level))
            {
                filter &= builder.Eq(log => log.Level, level);
            }

            if (additionalData != null)
            {
                filter &= builder.Eq(log => log.AdditionalData, additionalData);
            }

            await _loggingRepository.DeleteLogsAsync(filter);
        }
    }
}
