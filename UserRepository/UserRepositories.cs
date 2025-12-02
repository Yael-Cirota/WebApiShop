using Entities;
//using Microsoft.EntityFrameworkCore;
using Repositories;
//using System.Linq;
//using System.Text.Json;

namespace UserRepository
{
    public class UserRepositories : IUserRepositories
    {
        ShopContext _dbContext;

        public UserRepositories(ShopContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return _dbContext.Users;
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

        public async Task<User> FindUser(User user)
        {
            try
            {
                return await _dbContext.Users.FindAsync(user);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async void UpdateUser(int id, User user)
        {
            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync();
        }

    }
}
