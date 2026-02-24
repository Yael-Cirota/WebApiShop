using AutoMapper;
using DTO_s;
using Entities;
using Repository;

namespace Service
{
    public class UserServices : IUserServices
    {
        private readonly IUserRepository _userRepositories;
        private readonly IPasswordServices _passwordServices;
        private readonly IMapper _mapper;

        public UserServices(IUserRepository userRepositories, IMapper mapper, IPasswordServices passwordServices)
        {
            _userRepositories = userRepositories;
            _passwordServices = passwordServices;
            _mapper = mapper;
        }

        public async Task<UserDTO> GetById(int id)
        {
            Entities.User user = await _userRepositories.GetById(id);
            UserDTO userDTO = _mapper.Map<Entities.User, UserDTO>(user);
            return userDTO;
        }

        public async Task<UserDTO> AddUser(UserDTO user, string password)
        {
            if (_passwordServices.GetStrength(password).Strength < 2)
                return null;
            Entities.User userEntity = _mapper.Map<UserDTO, Entities.User>(user);
            userEntity.Password = password;
            Entities.User res = await _userRepositories.AddUser(userEntity);
            UserDTO userDTO = _mapper.Map<Entities.User, UserDTO>(res);
            return userDTO;
        }
        public async Task<UserDTO> FindUser(LoginUser user)
        {
            Entities.User res = await _userRepositories.FindUser(user);
            UserDTO userDTO = _mapper.Map<Entities.User, UserDTO>(res);
            return userDTO;
        }

        public async Task<bool> UpdateUser(int id, UserDTO user, string password)
        {
            Password password1 = _passwordServices.GetStrength(password);
            if (password1.Strength < 2)
                return false;
            Entities.User userToUpdate = _mapper.Map<UserDTO, Entities.User>(user);
            userToUpdate.Id = id;
            userToUpdate.Password = password;
            await _userRepositories.UpdateUser(userToUpdate);
            return true;
        }
    }
}
