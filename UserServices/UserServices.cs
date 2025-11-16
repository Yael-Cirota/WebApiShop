using Entities;
using UserRepository;

namespace Service
{
    public class UserServices : IUserServices
    {
        IUserRepositories _iUserRepositories;

        public UserServices(IUserRepositories userRepositories)
        {
            _iUserRepositories = userRepositories;
        }

        public string GetById()
        {
            return _iUserRepositories.GetById();
        }
        public IEnumerable<string> GetUsers()
        {
            return _iUserRepositories.GetUsers();
        }
        public User AddUser(User user)
        {
            return _iUserRepositories.AddUser(user);
        }
        public User FindUser(User user)
        {
            return _iUserRepositories.FindUser(user);
        }
        public Password UpdateUser(int id, User user)
        {
            var result = Zxcvbn.Core.EvaluatePassword(user.Password);
            if (result.Score >= 2)
                _iUserRepositories.UpdateUser(id, user);
            Password password = new Password { Passwrd = user.Password, Strength = result.Score };
            return password;
        }

    }
}
