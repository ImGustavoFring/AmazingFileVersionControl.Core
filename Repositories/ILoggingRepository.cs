/**
 * @file ILoggingRepository.cs
 * @brief Интерфейс для репозитория логирования.
 */

using AmazingFileVersionControl.Core.Models.LoggingEntities;
using MongoDB.Driver;

namespace AmazingFileVersionControl.Core.Repositories
{
    /**
     * @interface ILoggingRepository
     * @brief Интерфейс репозитория для управления логами.
     */
    public interface ILoggingRepository
    {
        /**
         * @brief Удалить лог по фильтру.
         * @param filter Фильтр для поиска лога.
         */
        Task DeleteLogAsync(FilterDefinition<LogEntity> filter);

        /**
         * @brief Удалить несколько логов по фильтру.
         * @param filter Фильтр для поиска логов.
         */
        Task DeleteLogsAsync(FilterDefinition<LogEntity> filter = null);

        /**
         * @brief Получить лог по фильтру.
         * @param filter Фильтр для поиска лога.
         * @return Найденная запись лога.
         */
        Task<LogEntity> GetLogAsync(FilterDefinition<LogEntity> filter);

        /**
         * @brief Получить список логов по фильтру.
         * @param filter Фильтр для поиска логов.
         * @return Список найденных логов.
         */
        Task<List<LogEntity>> GetLogsAsync(FilterDefinition<LogEntity> filter = null);

        /**
         * @brief Вставить запись лога в базу данных.
         * @param logEntry Запись лога.
         */
        Task InsertLogAsync(LogEntity logEntry);
    }
}
