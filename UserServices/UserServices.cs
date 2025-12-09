using Entities;
using UserRepository;

namespace Service
{
    public class UserServices : IUserServices
    {
        private readonly IUserRepositories _userRepositories;
        private readonly IPasswordServices _passwordServices;

        public UserServices(IUserRepositories userRepositories, IPasswordServices passwordServices)
        {
            _userRepositories = userRepositories;
            _passwordServices = passwordServices;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _userRepositories.GetUsers();
        }

        public async Task<User> GetById(int id)
        {
            return await _userRepositories.GetById(id);
        }

        public async Task<User> AddUser(User user)
        {
            if (_passwordServices.GetStrength(user.Password).Strength < 2)
                return null;
            return await _userRepositories.AddUser(user);
        }
        public async Task<User> FindUser(User user)
        {
            return await _userRepositories.FindUser(user);
        }
        public async void UpdateUser(int id, User user)
        {
            user.Id = id;
            _userRepositories.UpdateUser(id, user);
        }
    }
}
