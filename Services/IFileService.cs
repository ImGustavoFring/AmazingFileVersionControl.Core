using MongoDB.Bson;
using MongoDB.Driver.GridFS;

namespace AmazingFileVersionControl.Core.Services
{
    public interface IFileService
    {
        Task DeleteAllOwnerFilesAsync(string owner);
        Task DeleteFileAsync(string name, string owner, string project);
        Task DeleteFileByVersionAsync(string name, string owner, string project, long version);
        Task<Stream> DownloadFileAsync(string name, string owner, string project, long version = -1);
        Task<List<GridFSFileInfo>> GetAllOwnerFilesInfoAsync(string owner);
        Task<List<GridFSFileInfo>> GetFileInfoAsync(string name, string owner, string project);
        Task<GridFSFileInfo> GetFileInfoByVersionAsync(string name, string owner, string project, long version);
        Task UpdateAllOwnerFilesInfoAsync(string owner, BsonDocument updatedMetadata);
        Task UpdateFileInfoAsync(string name, string owner, string project, BsonDocument updatedMetadata);
        Task UpdateFileInfoByVersionAsync(string name, string owner, string project, long version, BsonDocument updatedMetadata);
        Task<ObjectId> UploadFileAsync(string name, string owner, string project, string type, Stream stream, string? description = null);
    }
}