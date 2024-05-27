using AmazingFileVersionControl.Core.Models.LoggingEntities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazingFileVersionControl.Core.Repositories
{
    public class LoggingRepository : ILoggingRepository
    {
        private readonly IMongoClient _client;
        private readonly IMongoDatabase _database;
        private readonly IMongoCollection<LogEntity> _logCollection;
        
        public LoggingRepository(IMongoClient client, string databaseName)
        {
            _client = client;
            _database = client.GetDatabase(databaseName);
            _logCollection = _database.GetCollection<LogEntity>("ApiLogs");
        }

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

        public async Task<LogEntity> GetLogAsync(FilterDefinition<LogEntity> filter)
        {
            return await _logCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<List<LogEntity>> GetLogsAsync(FilterDefinition<LogEntity> filter = null)
        {
            if (filter == null)
            {
                filter = Builders<LogEntity>.Filter.Empty;
            }
            return await _logCollection.Find(filter).ToListAsync();
        }

        public async Task DeleteLogAsync(FilterDefinition<LogEntity> filter)
        {
            await _logCollection.DeleteOneAsync(filter);
        }

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
