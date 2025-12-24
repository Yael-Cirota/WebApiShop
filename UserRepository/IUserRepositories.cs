using DTO_s;
using Entities;

namespace UserRepository
{
    public interface IUserRepositories
    {
        Task<User> AddUser(User user);
        Task<User?> FindUser(LoginUser user);
        Task<User> GetById(int id);
        Task<IEnumerable<User>> GetUsers();
        Task UpdateUser(User user);
    }
}