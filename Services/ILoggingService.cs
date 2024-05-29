/**
 * @file ILoggingService.cs
 * @brief Интерфейс для сервиса логирования.
 */

using AmazingFileVersionControl.Core.Models.LoggingEntities;
using MongoDB.Bson;

namespace AmazingFileVersionControl.Core.Services
{
    /**
     * @interface ILoggingService
     * @brief Интерфейс сервиса для управления логами.
     */
    public interface ILoggingService
    {
        /**
         * @brief Удалить лог по идентификатору.
         * @param id Идентификатор лога.
         */
        Task DeleteLogByIdAsync(string id);

        /**
         * @brief Удалить несколько логов по фильтрам.
         * @param controller Имя контроллера.
         * @param action Имя действия.
         * @param startDate Дата начала фильтрации.
         * @param endDate Дата окончания фильтрации.
         * @param level Уровень логирования.
         * @param additionalData Дополнительные данные.
         */
        Task DeleteLogsAsync(string controller = null, string action = null, DateTime? startDate = null, DateTime? endDate = null, string level = null, BsonDocument additionalData = null);

        /**
         * @brief Получить лог по идентификатору.
         * @param id Идентификатор лога.
         * @return Найденная запись лога.
         */
        Task<LogEntity> GetLogByIdAsync(string id);

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
        Task<List<LogEntity>> GetLogsAsync(string controller = null, string action = null, DateTime? startDate = null, DateTime? endDate = null, string level = null, BsonDocument additionalData = null);

        /**
         * @brief Логирование события.
         * @param controller Имя контроллера.
         * @param action Имя действия.
         * @param message Сообщение лога.
         * @param level Уровень логирования.
         * @param additionalData Дополнительные данные.
         */
        Task LogAsync(string controller, string action, string message, string level = "Info", BsonDocument additionalData = null);
    }
}
