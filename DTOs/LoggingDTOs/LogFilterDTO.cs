using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazingFileVersionControl.Core.DTOs.LoggingDTOs
{
    public class LogFilterDTO
    {
        public string? Controller { get; set; }
        public string? Action { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Level { get; set; }
        public string? AdditionalData { get; set; }
    }
}
