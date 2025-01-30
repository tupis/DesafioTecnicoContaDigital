using Application.Dto;
using Application.DTO.UserDto;
using AutoMapper;
using Domain.Entities;
using Infraestructure.Repositories;
using Infraestructure.Utils;
using System.Collections;

namespace Application.Services
{
    public class UserService(
        UserRepository userRespository, 
        IMapper mapper, 
        HashPassword hashPassword, 
        AccountService accountService)
    {
        private readonly UserRepository _userRepository = userRespository;
        private readonly IMapper _mapper = mapper;
        private readonly HashPassword _hashPassword = hashPassword;
        // private readonly AccountService _accountService = accountService;

        //public async Task<CreateUserDtoResponse> CreateUser(CreateUserDto createUserDto)
        public async Task<CreateUserDtoResponse> CreateUser(CreateUserDto createUserDto)
        {
            createUserDto.Password = _hashPassword.Hash(createUserDto.Password);
            var user = _mapper.Map<User>(createUserDto);
            var account = _mapper.Map<Account>(new CreateAccountDto
            {
                UserId = user.Id,
                Balance = 0
            });
            
            var savedUser = await _userRepository.CreateAsync(user, account);
            
            return _mapper.Map<CreateUserDtoResponse>(savedUser);;
        }

        public async Task<IEnumerable<CreateUserDtoResponse>> GetAllUsers()
        {
            var users = await _userRepository.GetAllUsers();
            return _mapper.Map<IEnumerable<CreateUserDtoResponse>>(users);
        }

        public async Task<CreateUserDtoResponse> GetUserById(Guid id)
        {
            var user = await _userRepository.FindById(id);
            return _mapper.Map<CreateUserDtoResponse>(user);
        }
    }
}
