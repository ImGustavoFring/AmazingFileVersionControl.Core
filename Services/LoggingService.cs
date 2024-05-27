using AmazingFileVersionControl.Core.Models.LoggingEntities;
using AmazingFileVersionControl.Core.Repositories;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazingFileVersionControl.Core.Services
{
    public class LoggingService : ILoggingService
    {
        private readonly ILoggingRepository _loggingRepository;

        public LoggingService(ILoggingRepository loggingRepository)
        {
            _loggingRepository = loggingRepository;
        }

        public async Task LogAsync(string controller, string action, string message, string level = "Info", BsonDocument additionalData = null)
        {
            try
            {
                var logEntry = new LogEntity
                {
                    Controller = controller,
                    Action = action,
                    Message = message,
                    Level = level,
                    AdditionalData = additionalData
                };
                await _loggingRepository.InsertLogAsync(logEntry);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to log the entry.", ex);
            }
        }

        public async Task<LogEntity> GetLogByIdAsync(string id)
        {
            var filter = Builders<LogEntity>.Filter.Eq(log => log.Id, id);
            return await _loggingRepository.GetLogAsync(filter);
        }

        public async Task<List<LogEntity>> GetLogsAsync(string controller = null,
            string action = null,
            DateTime? startDate = null, DateTime? endDate = null,
            string level = null,
            BsonDocument additionalData = null)
        {
            var builder = Builders<LogEntity>.Filter;
            var filter = builder.Empty;

            if (!string.IsNullOrEmpty(controller))
            {
                filter &= builder.Eq(log => log.Controller, controller);
            }

            if (!string.IsNullOrEmpty(action))
            {
                filter &= builder.Eq(log => log.Action, action);
            }

            if (startDate.HasValue)
            {
                filter &= builder.Gte(log => log.Timestamp, startDate.Value);
            }

            if (endDate.HasValue)
            {
                filter &= builder.Lte(log => log.Timestamp, endDate.Value);
            }

            if (!string.IsNullOrEmpty(level))
            {
                filter &= builder.Eq(log => log.Level, level);
            }

            if (additionalData != null)
            {
                filter &= builder.Eq(log => log.AdditionalData, additionalData);
            }

            return await _loggingRepository.GetLogsAsync(filter);
        }

        public async Task DeleteLogByIdAsync(string id)
        {
            var filter = Builders<LogEntity>.Filter.Eq(log => log.Id, id);
            await _loggingRepository.DeleteLogAsync(filter);
        }

        public async Task DeleteLogsAsync(string controller = null,
            string action = null,
            DateTime? startDate = null, DateTime? endDate = null,
            string level = null,
            BsonDocument additionalData = null)
        {
            var builder = Builders<LogEntity>.Filter;
            var filter = builder.Empty;

            if (!string.IsNullOrEmpty(controller))
            {
                filter &= builder.Eq(log => log.Controller, controller);
            }

            if (!string.IsNullOrEmpty(action))
            {
                filter &= builder.Eq(log => log.Action, action);
            }

            if (startDate.HasValue)
            {
                filter &= builder.Gte(log => log.Timestamp, startDate.Value);
            }

            if (endDate.HasValue)
            {
                filter &= builder.Lte(log => log.Timestamp, endDate.Value);
            }

            if (!string.IsNullOrEmpty(level))
            {
                filter &= builder.Eq(log => log.Level, level);
            }

            if (additionalData != null)
            {
                filter &= builder.Eq(log => log.AdditionalData, additionalData);
            }

            await _loggingRepository.DeleteLogsAsync(filter);
        }
    }
}
