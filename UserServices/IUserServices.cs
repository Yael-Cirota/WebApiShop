using Entities;

namespace Service
{
    public interface IUserServices
    {
        User AddUser(User user);
        User FindUser(User user);
        string GetById();
        IEnumerable<string> GetUsers();
        void UpdateUser(int id, User user);
    }
}