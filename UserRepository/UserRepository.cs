using DTO_s;
using Entities;
using Microsoft.EntityFrameworkCore;
using Repositories;
using System.Linq;
using System.Text.Json;

namespace Repositories
{
    public class UserRepository : IUserRepository
    {
        ShopContext _dbContext;

        public UserRepository(ShopContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> GetById(int id)
        {
            return await _dbContext.Users.FindAsync(id);
        }

        public async Task<User> AddUser(User user)
        {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
            return user;
        }

        public async Task<User?> FindUser(LoginUser user)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(
                x => x.Email == user.Email && x.Password == user.Password
             );
        }

        public async Task UpdateUser(User user)
        {
            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync();
        }
    }
}
