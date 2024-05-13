using AmazingFileVersionControl.Core.Models.LoggingEntities;

namespace AmazingFileVersionControl.Core.Repositories
{
    public interface ILoggingRepository
    {
        Task InsertLogAsync(LogEntity logEntry);
    }
}