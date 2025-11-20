using Entities;
using UserRepository;

namespace Service
{
    public class UserServices : IUserServices
    {
        private readonly IUserRepositories _userRepositories;

        public UserServices(IUserRepositories userRepositories)
        {
            _userRepositories = userRepositories;
        }

        public string GetById()
        {
            return _userRepositories.GetById();
        }
        public IEnumerable<string> GetUsers()
        {
            return _userRepositories.GetUsers();
        }
        public User AddUser(User user)
        {
            return _userRepositories.AddUser(user);
        }
        public User FindUser(User user)
        {
            return _userRepositories.FindUser(user);
        }
        public Password UpdateUser(int id, User user)
        {
            var result = Zxcvbn.Core.EvaluatePassword(user.Password);
            if (result.Score >= 2)
                _userRepositories.UpdateUser(id, user);
            Password password = new Password { PasswordValue = user.Password, Strength = result.Score };
            return password;
        }
    }
}
