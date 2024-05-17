using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazingFileVersionControl.Core.DTOs.FileDTOs
{
    public class UploadFileDto
    {
        public string Name { get; set; }
        public string Owner { get; set; }
        public string Type { get; set; }
        public string Project { get; set; }
        public IFormFile File { get; set; }
        public string Description { get; set; }
        public long? Version { get; set; }
    }

    public class FileInfoDto
    {
        public string Name { get; set; }
        public string Owner { get; set; }
        public string Type { get; set; }
        public string Project { get; set; }
        public long? Version { get; set; }
    }

    public class UpdateFileInfoDto
    {
        public string Name { get; set; }
        public string Owner { get; set; }
        public string Type { get; set; }
        public string Project { get; set; }
        public long Version { get; set; }
        public string UpdatedMetadataJson { get; set; }
    }

    public class UpdateProjectFilesDto
    {
        public string Owner { get; set; }
        public string Project { get; set; }
        public string UpdatedMetadataJson { get; set; }
    }

    public class DeleteFileDto
    {
        public string Name { get; set; }
        public string Owner { get; set; }
        public string Type { get; set; }
        public string Project { get; set; }
        public long Version { get; set; }
    }

}
