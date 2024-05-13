using AmazingFileVersionControl.Core.Models.UserDbEntities;
using System.Linq.Expressions;

namespace AmazingFileVersionControl.Core.Repositories
{
    public interface IUserRepository
    {
        Task AddAsync(UserEntity user);
        Task DeleteManyByFilterAsync(Expression<Func<UserEntity, bool>> filter);
        Task DeleteOneByFilterAsync(Expression<Func<UserEntity, bool>> filter);
        Task<List<UserEntity>> GetManyByFilterAsync(Expression<Func<UserEntity, bool>> filter);
        Task<UserEntity> GetOneByFilterAsync(Expression<Func<UserEntity, bool>> filter);
        Task UpdateManyByFilterAsync(Expression<Func<UserEntity, bool>> filter, Action<UserEntity> updateAction);
        Task UpdateOneByFilterAsync(Expression<Func<UserEntity, bool>> filter, Action<UserEntity> updateAction);
    }
}