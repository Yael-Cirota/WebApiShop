using AutoMapper;
using DTO_s;
using Entities;
using Microsoft.Azure.Documents;
using Repository;

namespace Service
{
    public class UserServices :IUserServices
    {
        private readonly IUserRepository _userRepositories;
        private readonly IPasswordServices _passwordServices;
        private readonly IMapper _iMapper;


        public UserServices(IUserRepository userRepositories, IMapper mapper, IPasswordServices passwordServices)
        {
            _userRepositories = userRepositories;
            _passwordServices = passwordServices;
            _iMapper = mapper;
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
        public async Task<UserDTO> FindUser(LoginUser user)
        {
            //Entities.User userLogin = _iMapper.Map<UserDTO, Entities.User>(user);
            Entities.User res = await _userRepositories.FindUser(user);
            UserDTO userDTO = _iMapper.Map<Entities.User, UserDTO>(res);
            return userDTO;
        }
<<<<<<< HEAD
        public async Task UpdateUser(int id, User user)
        {
            await _userRepositories.UpdateUser(id, user);
=======
        public async Task<bool> UpdateUser(int id, UserDTO user, string password)
        {
            Password password1 = _passwordServices.GetStrength(password);
            if (password1.Strength < 2)
                return false;
            Entities.User userToUpdate = _iMapper.Map<UserDTO, Entities.User>(user);
            userToUpdate.Id = id;
            userToUpdate.Password = password;
            await _userRepositories.UpdateUser(userToUpdate);
            return true;
>>>>>>> d27a0d75bc717bf29ce1559500c1a220865eb938
        }
    }
}
