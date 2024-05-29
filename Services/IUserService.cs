using AmazingFileVersionControl.Core.Models.UserDbEntities;

namespace AmazingFileVersionControl.Core.Services
{
    public interface IUserService
    {
        Task ChangeEmail(string userId, string newEmail);
        Task ChangeLogin(string userId, string newLogin);
        Task ChangePassword(string userId, string newPassword);
        Task ChangeRole(string userId, RoleInSystem newRole);
        Task DeleteById(string userId);
        Task<List<UserEntity>> GetAll();
        Task<List<UserEntity>> GetAllByEmailSubstring(string emailSubstring);
        Task<List<UserEntity>> GetAllByLoginSubstring(string loginSubstring);
        Task<UserEntity> GetById(string userId);
        Task<UserEntity> GetByLogin(string userLogin);
    }
}