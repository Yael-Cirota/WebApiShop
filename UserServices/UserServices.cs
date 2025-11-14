using Entities;
using UserRepository;

namespace Service
{
    public class UserServices
    {
        UserRepositories userRepositories = new UserRepositories();
        public string GetById()
        {
            return userRepositories.GetById();
        }
        public IEnumerable<string> GetUsers()
        {
            return userRepositories.GetUsers();
        }
        public User AddUser(User user)
        {
            return userRepositories.AddUser(user);
        }
        public User FindUser(User user)
        {
            return userRepositories.FindUser(user);
        }
        public void UpdateUser(int id, User user)
        {
            userRepositories.UpdateUser(id, user);
        }

    }
}
