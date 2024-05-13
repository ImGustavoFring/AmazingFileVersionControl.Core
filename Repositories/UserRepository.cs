using AmazingFileVersionControl.Core.Contexts;
using AmazingFileVersionControl.Core.Models.UserDbEntities;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AmazingFileVersionControl.Core.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserDbContext _context;

        public UserRepository(UserDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(UserEntity user)
        {
            try
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to add user.", ex);
            }
        }

        public async Task<UserEntity> GetOneByFilterAsync(Expression<Func<UserEntity, bool>> filter)
        {
            try
            {
                return await _context.Users.FirstOrDefaultAsync(filter);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve user.", ex);
            }
        }

        public async Task<List<UserEntity>> GetManyByFilterAsync(Expression<Func<UserEntity, bool>> filter)
        {
            try
            {
                return await _context.Users.Where(filter).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve users by filter.", ex);
            }
        }

        public async Task UpdateOneByFilterAsync(Expression<Func<UserEntity, bool>> filter, Action<UserEntity> updateAction)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(filter);
                if (user != null)
                {
                    updateAction(user);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to update user.", ex);
            }
        }

        public async Task UpdateManyByFilterAsync(Expression<Func<UserEntity, bool>> filter, Action<UserEntity> updateAction)
        {
            try
            {
                var users = await _context.Users.Where(filter).ToListAsync();
                foreach (var user in users)
                {
                    updateAction(user);
                }
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to update users by filter.", ex);
            }
        }

        public async Task DeleteOneByFilterAsync(Expression<Func<UserEntity, bool>> filter)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(filter);

                if (user != null)
                {
                    _context.Users.Remove(user);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to delete user.", ex);
            }
        }

        public async Task DeleteManyByFilterAsync(Expression<Func<UserEntity, bool>> filter)
        {
            try
            {
                var users = _context.Users.Where(filter);
                _context.Users.RemoveRange(users);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to delete users by filter.", ex);
            }
        }
    }
}
