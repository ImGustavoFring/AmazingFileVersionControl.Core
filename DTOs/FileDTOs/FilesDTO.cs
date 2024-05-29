/**
 * @file FilesDTO.cs
 * @brief DTO для операций с файлами.
 */

using Microsoft.AspNetCore.Http;

namespace AmazingFileVersionControl.Core.DTOs.FileDTOs
{
    /**
     * @class FileUploadDTO
     * @brief DTO для загрузки файлов.
     */
    public class FileUploadDTO : FileQueryWithVersionDTO
    {
        /** @brief Загружаемый файл. */
        public IFormFile File { get; set; }

        /** @brief Описание файла. */
        public string? Description { get; set; }
    }

    /**
     * @class FileQueryDTO
     * @brief DTO для запроса информации о файле.
     */
    public class FileQueryDTO
    {
        /** @brief Имя файла. */
        public string Name { get; set; }

        /** @brief Тип файла. */
        public string Type { get; set; }

        /** @brief Проект, к которому относится файл. */
        public string Project { get; set; }

        /** @brief Владелец файла. */
        public string? Owner { get; set; }
    }

    /**
     * @class FileQueryWithVersionDTO
     * @brief DTO для запроса информации о файле с указанием версии.
     */
    public class FileQueryWithVersionDTO : FileQueryDTO
    {
        /** @brief Версия файла. */
        public long? Version { get; set; }
    }

    /**
     * @class FileUpdateDTO
     * @brief DTO для обновления информации о файле.
     */
    public class FileUpdateDTO : FileQueryDTO
    {
        /** @brief Обновленные метаданные файла. */
        public string UpdatedMetadata { get; set; }
    }

    /**
     * @class FileUpdateWithVersionDTO
     * @brief DTO для обновления информации о файле с указанием версии.
     */
    public class FileUpdateWithVersionDTO : FileQueryWithVersionDTO
    {
        /** @brief Обновленные метаданные файла. */
        public string UpdatedMetadata { get; set; }
    }

    /**
     * @class UpdateAllFilesInProjectDTO
     * @brief DTO для обновления всех файлов в проекте.
     */
    public class UpdateAllFilesInProjectDTO
    {
        /** @brief Обновленные метаданные файлов. */
        public string UpdatedMetadata { get; set; }

        /** @brief Проект, к которому относятся файлы. */
        public string Project { get; set; }

        /** @brief Владелец файлов. */
        public string? Owner { get; set; }
    }

    /**
     * @class UpdateAllFilesDTO
     * @brief DTO для обновления всех файлов.
     */
    public class UpdateAllFilesDTO
    {
        /** @brief Обновленные метаданные файлов. */
        public string UpdatedMetadata { get; set; }

        /** @brief Владелец файлов. */
        public string? Owner { get; set; }
    }
}
