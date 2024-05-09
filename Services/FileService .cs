using AmazingFileVersionControl.Core.Repositories;
using MongoDB.Bson;
using MongoDB.Driver.GridFS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazingFileVersionControl.Core.Services
{
    public class FileService : IFileService
    {
        private readonly IFileRepository _fileRepository;

        public FileService(IFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
        }

        public async Task<ObjectId> UploadFileAsync(string name,
            string owner, string project,
            string type, Stream stream,
            string? description = null)
        {
            try
            {
                long lastVersionNumber = await GetLastVersionNumberAsync(name, owner, project) + 1;

                var metadata = new BsonDocument
                {
                    { "file_type", type},
                    { "owner", owner },
                    { "project", project },
                    { "version_number", lastVersionNumber},
                    { "version_description", description ?? "No description provided" }
                };

                return await _fileRepository.UploadAsync(name, stream, metadata);
            }
            catch (Exception ex)
            {
                throw new Exception("Error uploading file.", ex);
            }
        }

        public async Task<Stream> DownloadFileAsync(string name,
            string owner, string project,
            long version = -1)
        {
            try
            {
                long lastVersionNumber = await GetLastVersionNumberAsync(name, owner, project);

                var query = new BsonDocument
                {
                    { "filename", name },
                    { "metadata.owner", owner },
                    { "metadata.project", project }
                };

                if (version < 0)
                {
                    query.Add("metadata.version_number", lastVersionNumber);
                }
                else
                {
                    query.Add("metadata.version_number", version);
                }

                return await _fileRepository.DownloadAsync(query);
            }
            catch (Exception ex)
            {
                throw new Exception("Error downloading file.", ex);
            }
        }

        public async Task<GridFSFileInfo> GetFileInfoByVersionAsync(string name,
            string owner, string project,
            long version)
        {
            try
            {
                var query = new BsonDocument
                {
                    { "filename", name },
                    { "metadata.owner", owner },
                    { "metadata.project", project }
                };

                if (version != -1)
                {
                    query.Set("metadata.version_number", version);
                }

                return await _fileRepository.GetOneAsync(query);
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting file info by version.", ex);
            }
        }

        public async Task<List<GridFSFileInfo>> GetFileInfoAsync(string name,
            string owner, string project)
        {
            try
            {
                var query = new BsonDocument
                {
                    { "filename", name },
                    { "metadata.owner", owner },
                    { "metadata.project", project }
                };

                return await _fileRepository.GetManyAsync(query);
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting all files info by owner.", ex);
            }
        }

        public async Task<List<GridFSFileInfo>> GetAllOwnerFilesInfoAsync(string owner)
        {
            try
            {
                var query = new BsonDocument
                {
                    { "metadata.owner", owner }
                };

                return await _fileRepository.GetManyAsync(query);
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting all files info by owner.", ex);
            }
        }

        public async Task UpdateFileInfoByVersionAsync(string name,
            string owner, string project,
            long version, BsonDocument updatedMetadata)
        {
            try
            {
                var query = new BsonDocument
                {
                    { "filename", name },
                    { "metadata.owner", owner },
                    { "metadata.project", project},
                    { "metadata.version_number", version}
                };

                await _fileRepository.UpdateManyAsync(query, updatedMetadata);
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating file info version.", ex);
            }
        }

        public async Task UpdateFileInfoAsync(string name,
            string owner, string project,
            BsonDocument updatedMetadata)
        {
            try
            {
                var query = new BsonDocument
                {
                    { "filename", name },
                    { "metadata.owner", owner },
                    { "metadata.project", project}
                };

                await _fileRepository.UpdateManyAsync(query, updatedMetadata);
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating file info.", ex);
            }
        }

        public async Task UpdateAllOwnerFilesInfoAsync(string owner,
            BsonDocument updatedMetadata)
        {
            try
            {
                var query = new BsonDocument
                {
                    { "metadata.owner", owner}
                };

                await _fileRepository.UpdateManyAsync(query, updatedMetadata);
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating multiple files metadata.", ex);
            }
        }

        public async Task DeleteFileByVersionAsync(string name,
            string owner, string project,
            long version)
        {
            try
            {
                var query = new BsonDocument
                {
                    { "filename", name },
                    { "metadata.owner", owner },
                    { "metadata.project", project },
                    { "metadata.version_number", version }
                };

                await _fileRepository.DeleteOneAsync(query);
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting file version.", ex);
            }
        }

        public async Task DeleteFileAsync(string name,
            string owner, string project)
        {
            try
            {
                var query = new BsonDocument
                {
                    { "filename", name },
                    { "metadata.owner", owner },
                    { "metadata.project", project }
                };

                await _fileRepository.DeleteManyAsync(query);
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting file by owner.", ex);
            }
        }

        public async Task DeleteAllOwnerFilesAsync(string owner)
        {
            try
            {
                var query = new BsonDocument
                {
                    { "metadata.owner", owner}
                };
                await _fileRepository.DeleteManyAsync(query);
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting multiple files by owner.", ex);
            }
        }

        private async Task<long> GetLastVersionNumberAsync(string fileName,
            string owner, string project)
        {
            try
            {
                var query = new BsonDocument
                {
                    { "filename", fileName },
                    { "metadata.owner", owner },
                    { "metadata.project", project }
                };
                var files = await _fileRepository.GetManyAsync(query);

                var sortedFiles = files.OrderByDescending(f => f.Metadata["version_number"].AsInt64);

                var lastFile = sortedFiles.FirstOrDefault();

                return lastFile?.Metadata["version_number"].AsInt64 ?? 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting next version number.", ex);
            }
        }
    }
}
