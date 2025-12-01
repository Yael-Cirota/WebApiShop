using Entities;

namespace Service
{
    public interface IUserServices
    {
        Task<User> AddUser(User user);
        Task<User> FindUser(User user);
        Task<User> GetById(int id);
        Task<IEnumerable<User>> GetUsers();
        Task<Password> UpdateUser(int id, User user);
    }
}