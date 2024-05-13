using AmazingFileVersionControl.Core.Models.LoggingEntities;
using AmazingFileVersionControl.Core.Repositories;
using MongoDB.Bson;
using System;
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
    }
}
