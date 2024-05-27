using AmazingFileVersionControl.Core.Models.LoggingEntities;
using MongoDB.Bson;

namespace AmazingFileVersionControl.Core.Services
{
    public interface ILoggingService
    {
        Task DeleteLogByIdAsync(string id);
        Task DeleteLogsAsync(string controller = null, string action = null, DateTime? startDate = null, DateTime? endDate = null, string level = null, BsonDocument additionalData = null);
        Task<LogEntity> GetLogByIdAsync(string id);
        Task<List<LogEntity>> GetLogsAsync(string controller = null, string action = null, DateTime? startDate = null, DateTime? endDate = null, string level = null, BsonDocument additionalData = null);
        Task LogAsync(string controller, string action, string message, string level = "Info", BsonDocument additionalData = null);
    }
}