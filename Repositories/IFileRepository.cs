using MongoDB.Bson;
using MongoDB.Driver.GridFS;

namespace AmazingFileVersionControl.Core.Repositories
{
    public interface IFileRepository
    {
        Task DeleteManyAsync(BsonDocument query);
        Task DeleteOneAsync(BsonDocument query);
        Task<Stream> DownloadAsync(BsonDocument query);
        Task<List<GridFSFileInfo>> GetManyAsync(BsonDocument query);
        Task<GridFSFileInfo> GetOneAsync(BsonDocument query);
        Task UpdateManyAsync(BsonDocument query, BsonDocument updatedMetadata);
        Task UpdateOneAsync(BsonDocument query, BsonDocument updatedMetadata);
        Task<ObjectId> UploadAsync(string fileName, Stream stream, BsonDocument metadata);
    }
}