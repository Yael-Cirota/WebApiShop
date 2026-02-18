using Entities;

namespace UserRepository
{
    public interface IUserRepositories
    {
        Task<User> AddUser(User user);
        Task<User> FindUser(User user);
        Task<User> GetById(int id);
        Task<IEnumerable<User>> GetUsers();
        Task UpdateUser(int id, User user);
    }
}