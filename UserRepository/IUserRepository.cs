using DTO_s;
using Entities;

namespace Repositories
{
    public interface IUserRepository
    {
        Task<User> AddUser(User user);
        Task<User?> FindUser(LoginUser user);
        Task<User> GetById(int id);
        Task UpdateUser(User user);
    }
}