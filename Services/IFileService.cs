using MongoDB.Bson;
using MongoDB.Driver.GridFS;

namespace AmazingFileVersionControl.Core.Services
{
    public interface IFileService
    {
        Task DeleteAllFilesAsync(string owner);
        Task DeleteFileAsync(string name, string owner, string type, string project);
        Task DeleteFileByVersionAsync(string name, string owner, string type, string project, long version);
        Task DeleteProjectFilesAsync(string owner, string project);
        Task<(Stream, BsonDocument)> DownloadFileWithMetadataAsync(string name, string owner, string type, string project, long? version = null);
        Task<List<GridFSFileInfo>> GetAllFilesInfoAsync(string owner);
        Task<List<GridFSFileInfo>> GetFileInfoAsync(string name, string owner, string type, string project);
        Task<GridFSFileInfo> GetFileInfoByVersionAsync(string name, string owner, string type, string project, long version);
        Task<List<GridFSFileInfo>> GetProjectFilesInfoAsync(string owner, string project);
        Task UpdateAllFilesInfoAsync(string owner, BsonDocument updatedMetadata);
        Task UpdateFileInfoAsync(string name, string owner, string type, string project, BsonDocument updatedMetadata);
        Task UpdateFileInfoByProjectAsync(string owner, string project, BsonDocument updatedMetadata);
        Task UpdateFileInfoByVersionAsync(string name, string owner, string type, string project, long version, BsonDocument updatedMetadata);
        Task<ObjectId> UploadFileAsync(string name, string owner, string type, string project, Stream stream, string? description = null, long? version = null);
    }
}