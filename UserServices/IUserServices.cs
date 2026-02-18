using DTO_s;

namespace Service
{
    public interface IUserServices
    {
<<<<<<< HEAD
        Task<User> AddUser(User user);
        Task<User> FindUser(User user);
        Task<User> GetById(int id);
        Task<IEnumerable<User>> GetUsers();
        Task UpdateUser(int id, User user);
=======
        Task<UserDTO> AddUser(UserDTO user, string password);
        Task<UserDTO> FindUser(LoginUser user);
        Task<UserDTO> GetById(int id);
        Task<bool> UpdateUser(int id, UserDTO user, string password);
>>>>>>> d27a0d75bc717bf29ce1559500c1a220865eb938
    }
}