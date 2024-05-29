/**
 * @file LogFilterDTO.cs
 * @brief DTO для фильтрации логов.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazingFileVersionControl.Core.DTOs.LoggingDTOs
{
    /**
     * @class LogFilterDTO
     * @brief Класс DTO для передачи данных фильтрации логов.
     */
    public class LogFilterDTO
    {
        /** @brief Имя контроллера. */
        public string? Controller { get; set; }

        /** @brief Имя действия. */
        public string? Action { get; set; }

        /** @brief Дата начала для фильтрации. */
        public DateTime? StartDate { get; set; }

        /** @brief Дата окончания для фильтрации. */
        public DateTime? EndDate { get; set; }

        /** @brief Уровень логирования. */
        public string? Level { get; set; }

        /** @brief Дополнительные данные для фильтрации. */
        public string? AdditionalData { get; set; }
    }
}
