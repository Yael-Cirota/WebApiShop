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

<<<<<<< HEAD:UserRepository/UserRepositories.cs
        public async Task<IEnumerable<User>> GetUsers()
        {
            return await Task.FromResult(_dbContext.Users);
        }

=======
>>>>>>> d27a0d75bc717bf29ce1559500c1a220865eb938:UserRepository/UserRepository.cs
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

<<<<<<< HEAD:UserRepository/UserRepositories.cs
        public async Task UpdateUser(int id, User user)
=======
        public async Task UpdateUser(User user)
>>>>>>> d27a0d75bc717bf29ce1559500c1a220865eb938:UserRepository/UserRepository.cs
        {
            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync();
        }
    }
}
