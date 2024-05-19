using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazingFileVersionControl.Core.DTOs.FileDTOs
{
    public class FileUploadDTO
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Project { get; set; }
        public IFormFile File { get; set; }
        public string? Description { get; set; }
        public string? Owner { get; set; }
        public long? Version { get; set; }
    }

    public class FileQueryDTO
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Project { get; set; }
        public long? Version { get; set; }
        public string? Owner { get; set; }
    }

    public class FileUpdateDTO
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Project { get; set; }
        public string UpdatedMetadata { get; set; }
        public long? Version { get; set; }
        public string? Owner { get; set; }
    }

    public class UpdateAllFilesDTO
    {
        public string UpdatedMetadata { get; set; }
        public string Project { get; set; }
        public string? Owner { get; set; }
    }
}
