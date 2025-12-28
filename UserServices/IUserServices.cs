using DTO_s;

namespace Service
{
    public interface IUserServices
    {
        Task<UserDTO> AddUser(UserDTO user, string password);
        Task<UserDTO> FindUser(LoginUser user);
        Task<UserDTO> GetById(int id);
        Task<bool> UpdateUser(int id, UserDTO user, string password);
    }
}