using Entities;

namespace UserRepository
{
    public interface IUserRepositories
    {
        User AddUser(User user);
        User FindUser(User user);
        string GetById();
        IEnumerable<string> GetUsers();
        void UpdateUser(int id, User user);
    }
}