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
    public class LoggingService : ILoggingService
    {
        private readonly ILoggingRepository _loggingRepository;

        public LoggingService(ILoggingRepository loggingRepository)
        {
            _loggingRepository = loggingRepository;
        }

        public async Task LogAsync(string controller, string action, string message, string level = "Info", BsonDocument additionalData = null)
        {
            var logEntry = new LogEntry
            {
                Controller = controller,
                Action = action,
                Message = message,
                Level = level,
                AdditionalData = additionalData
            };
            await _loggingRepository.InsertLogAsync(logEntry);
        }
    }
}
