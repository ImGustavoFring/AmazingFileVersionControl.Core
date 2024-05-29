/**
 * @file LogEntity.cs
 * @brief Описание сущности логирования.
 */

using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazingFileVersionControl.Core.Models.LoggingEntities
{
    /**
     * @class LogEntity
     * @brief Класс, представляющий сущность логирования.
     */
    public class LogEntity
    {
        /** @brief Идентификатор лога. */
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        /** @brief Имя контроллера. */
        public string Controller { get; set; }

        /** @brief Имя действия. */
        public string Action { get; set; }

        /** @brief Временная метка лога. */
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        /** @brief Сообщение лога. */
        public string Message { get; set; }

        /** @brief Уровень логгирования. */
        public string Level { get; set; }

        /** @brief Дополнительные данные. */
        public BsonDocument AdditionalData { get; set; }
    }
}
