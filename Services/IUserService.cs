using AmazingFileVersionControl.Core.Models.UserDbEntities;

namespace AmazingFileVersionControl.Core.Services
{
    public interface IUserService
    {
        Task ChangeEmail(Guid userId, string newEmail);
        Task ChangeLogin(Guid userId, string newLogin);
        Task ChangePassword(Guid userId, string newPassword);
        Task ChangeRole(Guid userId, RoleInSystem newRole);
        Task DeleteById(Guid userId);
        Task<List<UserEntity>> GetAll();
        Task<List<UserEntity>> GetAllByEmailSubstring(string emailSubstring);
        Task<List<UserEntity>> GetAllByLoginSubstring(string loginSubstring);
        Task<UserEntity> GetById(Guid userId);
    }
}