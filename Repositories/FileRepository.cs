/**
 * @file FileRepository.cs
 * @brief Репозиторий для управления файлами.
 */

using MongoDB.Bson;
using MongoDB.Driver.GridFS;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazingFileVersionControl.Core.Repositories
{
    /**
     * @class FileRepository
     * @brief Класс репозитория для управления файлами.
     */
    public class FileRepository : IFileRepository
    {
        private readonly IMongoDatabase _database;
        private readonly IGridFSBucket _gridFSBucket;
        private readonly IMongoClient _client;

        /**
         * @brief Конструктор класса FileRepository.
         * @param client Клиент MongoDB.
         * @param databaseName Название базы данных.
         */
        public FileRepository(IMongoClient client, string databaseName)
        {
            _client = client;
            _database = _client.GetDatabase(databaseName);
            _gridFSBucket = new GridFSBucket(_database);
        }

        /**
         * @brief Загрузить файл в базу данных.
         * @param fileName Имя файла.
         * @param stream Поток данных файла.
         * @param metadata Метаданные файла.
         * @return Идентификатор загруженного файла.
         */
        public async Task<ObjectId> UploadAsync(string fileName, Stream stream, BsonDocument metadata)
        {
            if (stream == null || stream.Length == 0)
            {
                throw new ArgumentException("Stream is empty");
            }

            stream.Position = 0;

            try
            {
                var options = new GridFSUploadOptions { Metadata = metadata };

                return await _gridFSBucket.UploadFromStreamAsync(fileName, stream, options);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to upload file.", ex);
            }
        }

        /**
         * @brief Скачать файл из базы данных.
         * @param query Запрос для поиска файла.
         * @return Поток данных файла.
         */
        public async Task<Stream> DownloadAsync(BsonDocument query)
        {
            try
            {
                var cursor = await _gridFSBucket.FindAsync(query);
                var fileInfo = await cursor.FirstOrDefaultAsync();

                if (fileInfo == null)
                {
                    throw new FileNotFoundException("File not found.");
                }

                var stream = new MemoryStream();
                await _gridFSBucket.DownloadToStreamAsync(fileInfo.Id, stream);
                stream.Position = 0;

                return stream;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to download file.", ex);
            }
        }

        /**
         * @brief Получить информацию о файле.
         * @param query Запрос для поиска файла.
         * @return Информация о файле.
         */
        public async Task<GridFSFileInfo> GetOneAsync(BsonDocument query)
        {
            try
            {
                var cursor = await _gridFSBucket.FindAsync(query);
                return await cursor.FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to get file information.", ex);
            }
        }

        /**
         * @brief Получить информацию о нескольких файлах.
         * @param query Запрос для поиска файлов.
         * @return Список информации о файлах.
         */
        public async Task<List<GridFSFileInfo>> GetManyAsync(BsonDocument query)
        {
            try
            {
                var cursor = await _gridFSBucket.FindAsync(query);
                return await cursor.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to get files.", ex);
            }
        }

        /**
         * @brief Обновить информацию о файле.
         * @param query Запрос для поиска файла.
         * @param updatedMetadata Обновленные метаданные файла.
         */
        public async Task UpdateOneAsync(BsonDocument query, BsonDocument updatedMetadata)
        {
            try
            {
                var filesCollection = _database.GetCollection<BsonDocument>("fs.files");
                var fileInfo = await filesCollection.Find(query).FirstOrDefaultAsync();

                if (fileInfo != null)
                {
                    var updateDefinitions = new List<UpdateDefinition<BsonDocument>>();

                    if (updatedMetadata.Contains("filename"))
                    {
                        updateDefinitions.Add(Builders<BsonDocument>.Update.Set("filename", updatedMetadata["filename"]));
                        updatedMetadata.Remove("filename");
                    }

                    if (updatedMetadata.ElementCount > 0)
                    {
                        var existingMetadata = fileInfo["metadata"].AsBsonDocument;
                        var combinedMetadata = new BsonDocument(existingMetadata);

                        foreach (var element in updatedMetadata)
                        {
                            combinedMetadata[element.Name] = element.Value;
                        }

                        updateDefinitions.Add(Builders<BsonDocument>.Update.Set("metadata", combinedMetadata));
                    }

                    if (updateDefinitions.Count > 0)
                    {
                        var update = Builders<BsonDocument>.Update.Combine(updateDefinitions);
                        await filesCollection.UpdateOneAsync(query, update);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to update file metadata: " + ex.Message, ex);
            }
        }

        /**
         * @brief Обновить информацию о нескольких файлах.
         * @param query Запрос для поиска файлов.
         * @param updatedMetadata Обновленные метаданные файлов.
         */
        public async Task UpdateManyAsync(BsonDocument query, BsonDocument updatedMetadata)
        {
            try
            {
                var filesCollection = _database.GetCollection<BsonDocument>("fs.files");
                var cursor = filesCollection.Find(query).ToCursor();

                foreach (var fileInfo in await cursor.ToListAsync())
                {
                    var updateDefinitions = new List<UpdateDefinition<BsonDocument>>();

                    if (updatedMetadata.Contains("filename"))
                    {
                        updateDefinitions.Add(Builders<BsonDocument>.Update.Set("filename", updatedMetadata["filename"]));
                        updatedMetadata.Remove("filename");
                    }

                    if (updatedMetadata.ElementCount > 0)
                    {
                        var existingMetadata = fileInfo["metadata"].AsBsonDocument;
                        var combinedMetadata = new BsonDocument(existingMetadata);

                        foreach (var element in updatedMetadata)
                        {
                            combinedMetadata[element.Name] = element.Value;
                        }

                        updateDefinitions.Add(Builders<BsonDocument>.Update.Set("metadata", combinedMetadata));
                    }

                    if (updateDefinitions.Count > 0)
                    {
                        var update = Builders<BsonDocument>.Update.Combine(updateDefinitions);
                        await filesCollection.UpdateOneAsync(Builders<BsonDocument>.Filter.Eq("_id", fileInfo["_id"]), update);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to update files metadata: " + ex.Message, ex);
            }
        }

        /**
         * @brief Удалить файл.
         * @param query Запрос для поиска файла.
         */
        public async Task DeleteOneAsync(BsonDocument query)
        {
            try
            {
                var cursor = await _gridFSBucket.FindAsync(query);
                var fileInfo = await cursor.FirstOrDefaultAsync();

                if (fileInfo != null)
                {
                    await _gridFSBucket.DeleteAsync(fileInfo.Id);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to delete file.", ex);
            }
        }

        /**
         * @brief Удалить несколько файлов.
         * @param query Запрос для поиска файлов.
         */
        public async Task DeleteManyAsync(BsonDocument query)
        {
            try
            {
                var cursor = await _gridFSBucket.FindAsync(query);

                foreach (var fileInfo in await cursor.ToListAsync())
                {
                    await _gridFSBucket.DeleteAsync(fileInfo.Id);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to delete files.", ex);
            }
        }
    }
}
