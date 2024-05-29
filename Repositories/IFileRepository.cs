/**
 * @file IFileRepository.cs
 * @brief Интерфейс для репозитория файлов.
 */

using MongoDB.Bson;
using MongoDB.Driver.GridFS;

namespace AmazingFileVersionControl.Core.Repositories
{
    /**
     * @interface IFileRepository
     * @brief Интерфейс репозитория для управления файлами.
     */
    public interface IFileRepository
    {
        /**
         * @brief Удалить несколько файлов по запросу.
         * @param query Запрос для поиска файлов.
         */
        Task DeleteManyAsync(BsonDocument query);

        /**
         * @brief Удалить один файл по запросу.
         * @param query Запрос для поиска файла.
         */
        Task DeleteOneAsync(BsonDocument query);

        /**
         * @brief Скачать файл по запросу.
         * @param query Запрос для поиска файла.
         * @return Поток данных файла.
         */
        Task<Stream> DownloadAsync(BsonDocument query);

        /**
         * @brief Получить информацию о нескольких файлах по запросу.
         * @param query Запрос для поиска файлов.
         * @return Список информации о файлах.
         */
        Task<List<GridFSFileInfo>> GetManyAsync(BsonDocument query);

        /**
         * @brief Получить информацию об одном файле по запросу.
         * @param query Запрос для поиска файла.
         * @return Информация о файле.
         */
        Task<GridFSFileInfo> GetOneAsync(BsonDocument query);

        /**
         * @brief Обновить информацию о нескольких файлах по запросу.
         * @param query Запрос для поиска файлов.
         * @param updatedMetadata Обновленные метаданные файлов.
         */
        Task UpdateManyAsync(BsonDocument query, BsonDocument updatedMetadata);

        /**
         * @brief Обновить информацию об одном файле по запросу.
         * @param query Запрос для поиска файла.
         * @param updatedMetadata Обновленные метаданные файла.
         */
        Task UpdateOneAsync(BsonDocument query, BsonDocument updatedMetadata);

        /**
         * @brief Загрузить файл.
         * @param fileName Имя файла.
         * @param stream Поток данных файла.
         * @param metadata Метаданные файла.
         * @return Идентификатор загруженного файла.
         */
        Task<ObjectId> UploadAsync(string fileName, Stream stream, BsonDocument metadata);
    }
}
