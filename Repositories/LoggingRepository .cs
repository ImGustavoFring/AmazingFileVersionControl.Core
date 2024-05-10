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
        private readonly IMongoCollection<LogEntry> _logCollection;

        public LoggingRepository(IMongoClient client, string databaseName)
        {
            var database = client.GetDatabase(databaseName);
            _logCollection = database.GetCollection<LogEntry>("Logs");
        }

        public async Task InsertLogAsync(LogEntry logEntry)
        {
            await _logCollection.InsertOneAsync(logEntry);
        }
    }
}
