using Application.Dto;
using Application.DTO.UserDto;
using AutoMapper;
using Domain.Entities;
using Infraestructure.Repositories;

namespace Application.Services
{
    public class AccountService(AccountRepository accountRepository, IMapper mapper)
    {
        private readonly AccountRepository _accountRepository = accountRepository;
        private readonly IMapper _mapper = mapper;
        public float GetBalance()
        {
            return 0;
        }

        public async Task<Account> CreateAccount(CreateAccountDto createAccountDto)
        {
            var account = _mapper.Map<Account>(createAccountDto);
            return await _accountRepository.CreateAsync(account);
        }
    }
}
