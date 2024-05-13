using AmazingFileVersionControl.Core.Models.LoggingEntities;
using MongoDB.Driver;
using System;
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
    }
}
