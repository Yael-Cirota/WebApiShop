using AutoMapper;
using DTO_s;
using Entities;
using Microsoft.Azure.Documents;
using UserRepository;

namespace Service
{
    public class UserServices : IUserServices
    {
        private readonly IUserRepositories _userRepositories;
        private readonly IPasswordServices _passwordServices;
        private readonly IMapper _iMapper;


        public UserServices(IUserRepositories userRepositories,IMapper mapper, IPasswordServices passwordServices)
        {
            _userRepositories = userRepositories;
            _passwordServices = passwordServices;
            _iMapper = mapper;
        }

        public async Task<IEnumerable<UserDTO>> GetUsers()
        {
            IEnumerable<Entities.User> users =  await _userRepositories.GetUsers();
            IEnumerable<UserDTO> userResult = _iMapper.Map<IEnumerable<Entities.User>, IEnumerable<UserDTO>>(users);
            return userResult;
        }

        public async Task<UserDTO> GetById(int id)
        {
            Entities.User user = await _userRepositories.GetById(id);
            UserDTO userDTO = _iMapper.Map<Entities.User, UserDTO>(user);
            return userDTO;
        }

        public async Task<UserDTO> AddUser(UserDTO user, string password)
        {
            if (_passwordServices.GetStrength(password).Strength < 2)
                return null;
            Entities.User userEntity = _iMapper.Map<UserDTO, Entities.User>(user);
            userEntity.Password = password;
            Entities.User res = await _userRepositories.AddUser(userEntity);
            UserDTO userDTO = _iMapper.Map<Entities.User, UserDTO>(res);
            return userDTO;
        }
        public async Task<UserDTO> FindUser(UserDTO user)
        {
            //ליצור USERLOGIN
            Entities.User userLogin = _iMapper.Map<UserDTO, Entities.User>(user);
            Entities.User res = await _userRepositories.AddUser(userLogin);
            UserDTO userDTO = _iMapper.Map<Entities.User, UserDTO>(res);
            return userDTO;
        }
        public async void UpdateUser(int id, UserDTO user)
        {
            user.Id = id;
            Entities.User userToUpdate = _iMapper.Map<UserDTO, Entities.User>(user);
            await _userRepositories.UpdateUser(id, userToUpdate);
        }
    }
}
