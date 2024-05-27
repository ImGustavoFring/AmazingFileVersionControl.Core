using AmazingFileVersionControl.Core.Models.LoggingEntities;
using MongoDB.Driver;

namespace AmazingFileVersionControl.Core.Repositories
{
    public interface ILoggingRepository
    {
        Task DeleteLogAsync(FilterDefinition<LogEntity> filter);
        Task DeleteLogsAsync(FilterDefinition<LogEntity> filter = null);
        Task<LogEntity> GetLogAsync(FilterDefinition<LogEntity> filter);
        Task<List<LogEntity>> GetLogsAsync(FilterDefinition<LogEntity> filter = null);
        Task InsertLogAsync(LogEntity logEntry);
    }
}