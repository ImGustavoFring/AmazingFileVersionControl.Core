/**
 * @file LoggingRepository.cs
 * @brief Репозиторий для управления логами.
 */

using AmazingFileVersionControl.Core.Models.LoggingEntities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazingFileVersionControl.Core.Repositories
{
    /**
     * @class LoggingRepository
     * @brief Класс репозитория для управления логами.
     */
    public class LoggingRepository : ILoggingRepository
    {
        private readonly IMongoClient _client;
        private readonly IMongoDatabase _database;
        private readonly IMongoCollection<LogEntity> _logCollection;

        /**
         * @brief Конструктор класса LoggingRepository.
         * @param client Клиент MongoDB.
         * @param databaseName Название базы данных.
         */
        public LoggingRepository(IMongoClient client, string databaseName)
        {
            _client = client;
            _database = client.GetDatabase(databaseName);
            _logCollection = _database.GetCollection<LogEntity>("ApiLogs");
        }

        /**
         * @brief Вставить запись лога в базу данных.
         * @param logEntry Запись лога.
         */
        public async Task InsertLogAsync(LogEntity logEntry)
        {
            try
            {
                await _logCollection.InsertOneAsync(logEntry);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to insert log entry.", ex);
            }
        }

        /**
         * @brief Получить запись лога по фильтру.
         * @param filter Фильтр для поиска лога.
         * @return Найденная запись лога.
         */
        public async Task<LogEntity> GetLogAsync(FilterDefinition<LogEntity> filter)
        {
            return await _logCollection.Find(filter).FirstOrDefaultAsync();
        }

        /**
         * @brief Получить список логов по фильтру.
         * @param filter Фильтр для поиска логов.
         * @return Список найденных логов.
         */
        public async Task<List<LogEntity>> GetLogsAsync(FilterDefinition<LogEntity> filter = null)
        {
            if (filter == null)
            {
                filter = Builders<LogEntity>.Filter.Empty;
            }
            return await _logCollection.Find(filter).ToListAsync();
        }

        /**
         * @brief Удалить запись лога по фильтру.
         * @param filter Фильтр для поиска лога.
         */
        public async Task DeleteLogAsync(FilterDefinition<LogEntity> filter)
        {
            await _logCollection.DeleteOneAsync(filter);
        }

        /**
         * @brief Удалить список логов по фильтру.
         * @param filter Фильтр для поиска логов.
         */
        public async Task DeleteLogsAsync(FilterDefinition<LogEntity> filter = null)
        {
            if (filter == null)
            {
                filter = Builders<LogEntity>.Filter.Empty;
            }
            await _logCollection.DeleteManyAsync(filter);
        }
    }
}
