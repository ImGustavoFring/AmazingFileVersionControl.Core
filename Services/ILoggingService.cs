using MongoDB.Bson;

namespace AmazingFileVersionControl.Core.Services
{
    public interface ILoggingService
    {
        Task LogAsync(string controller, string action, string message, string level = "Info", BsonDocument additionalData = null);
    }
}