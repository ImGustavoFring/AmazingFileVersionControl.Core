/**
 * @file IFileService.cs
 * @brief Интерфейс для сервиса управления файлами.
 */

using MongoDB.Bson;
using MongoDB.Driver.GridFS;

namespace AmazingFileVersionControl.Core.Services
{
    /**
     * @interface IFileService
     * @brief Интерфейс сервиса для управления файлами.
     */
    public interface IFileService
    {
        /**
         * @brief Удалить все файлы пользователя.
         * @param owner Владелец файлов.
         */
        Task DeleteAllFilesAsync(string owner);

        /**
         * @brief Удалить файл.
         * @param name Имя файла.
         * @param owner Владелец файла.
         * @param type Тип файла.
         * @param project Проект, к которому относится файл.
         */
        Task DeleteFileAsync(string name, string owner, string type, string project);

        /**
         * @brief Удалить файл по версии.
         * @param name Имя файла.
         * @param owner Владелец файла.
         * @param type Тип файла.
         * @param project Проект, к которому относится файл.
         * @param version Версия файла.
         */
        Task DeleteFileByVersionAsync(string name, string owner, string type, string project, long version);

        /**
         * @brief Удалить все файлы проекта.
         * @param owner Владелец файлов.
         * @param project Проект, к которому относятся файлы.
         */
        Task DeleteProjectFilesAsync(string owner, string project);

        /**
         * @brief Скачать файл с метаданными.
         * @param name Имя файла.
         * @param owner Владелец файла.
         * @param type Тип файла.
         * @param project Проект, к которому относится файл.
         * @param version Версия файла.
         * @return Поток данных файла и информация о файле.
         */
        Task<(Stream, GridFSFileInfo)> DownloadFileWithMetadataAsync(string name, string owner, string type, string project, long? version = null);

        /**
         * @brief Получить информацию о всех файлах пользователя.
         * @param owner Владелец файлов.
         * @return Список информации о файлах.
         */
        Task<List<GridFSFileInfo>> GetAllFilesInfoAsync(string owner);

        /**
         * @brief Получить информацию о файле.
         * @param name Имя файла.
         * @param owner Владелец файла.
         * @param type Тип файла.
         * @param project Проект, к которому относится файл.
         * @return Список информации о файлах.
         */
        Task<List<GridFSFileInfo>> GetFileInfoAsync(string name, string owner, string type, string project);

        /**
         * @brief Получить информацию о файле по версии.
         * @param name Имя файла.
         * @param owner Владелец файла.
         * @param type Тип файла.
         * @param project Проект, к которому относится файл.
         * @param version Версия файла.
         * @return Информация о файле.
         */
        Task<GridFSFileInfo> GetFileInfoByVersionAsync(string name, string owner, string type, string project, long version);

        /**
         * @brief Получить информацию о всех файлах проекта.
         * @param owner Владелец файлов.
         * @param project Проект, к которому относятся файлы.
         * @return Список информации о файлах.
         */
        Task<List<GridFSFileInfo>> GetProjectFilesInfoAsync(string owner, string project);

        /**
         * @brief Обновить информацию о всех файлах пользователя.
         * @param owner Владелец файлов.
         * @param updatedMetadata Обновленные метаданные файлов.
         */
        Task UpdateAllFilesInfoAsync(string owner, BsonDocument updatedMetadata);

        /**
         * @brief Обновить информацию о файле.
         * @param name Имя файла.
         * @param owner Владелец файла.
         * @param type Тип файла.
         * @param project Проект, к которому относится файл.
         * @param updatedMetadata Обновленные метаданные файла.
         */
        Task UpdateFileInfoAsync(string name, string owner, string type, string project, BsonDocument updatedMetadata);

        /**
         * @brief Обновить информацию о всех файлах проекта.
         * @param owner Владелец файлов.
         * @param project Проект, к которому относятся файлы.
         * @param updatedMetadata Обновленные метаданные файлов.
         */
        Task UpdateFileInfoByProjectAsync(string owner, string project, BsonDocument updatedMetadata);

        /**
         * @brief Обновить информацию о файле по версии.
         * @param name Имя файла.
         * @param owner Владелец файла.
         * @param type Тип файла.
         * @param project Проект, к которому относится файл.
         * @param version Версия файла.
         * @param updatedMetadata Обновленные метаданные файла.
         */
        Task UpdateFileInfoByVersionAsync(string name, string owner, string type, string project, long version, BsonDocument updatedMetadata);

        /**
         * @brief Загрузить файл.
         * @param name Имя файла.
         * @param owner Владелец файла.
         * @param type Тип файла.
         * @param project Проект, к которому относится файл.
         * @param stream Поток данных файла.
         * @param description Описание файла.
         * @param version Версия файла.
         * @return Идентификатор загруженного файла.
         */
        Task<ObjectId> UploadFileAsync(string name, string owner, string type, string project, Stream stream, string? description = null, long? version = null);
    }
}
