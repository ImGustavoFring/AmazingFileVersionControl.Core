using Microsoft.AspNetCore.Http;

namespace AmazingFileVersionControl.Core.DTOs.FileDTOs
{
    public class FileUploadDTO : FileQueryWithVersionDTO
    {
        public IFormFile File { get; set; }
        public string? Description { get; set; }
       
    }

    public class FileQueryDTO
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Project { get; set; }
        public string? Owner { get; set; }
    }

    public class FileQueryWithVersionDTO : FileQueryDTO
    {
        public long? Version { get; set; }
    }

    public class FileUpdateDTO : FileQueryDTO
    {
        public string UpdatedMetadata { get; set; }
    }

    public class FileUpdateWithVersionDTO : FileQueryWithVersionDTO
    {
        public string UpdatedMetadata { get; set; }
    }

    public class UpdateAllFilesInProjectDTO
    {
        public string UpdatedMetadata { get; set; }
        public string Project { get; set; }
        public string? Owner { get; set; }
    }

    public class UpdateAllFilesDTO
    {
        public string UpdatedMetadata { get; set; }
        public string? Owner { get; set; }
    }
}
