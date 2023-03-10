using AutoMapper;
using BLL.Contracts;
using BLL.Errors;
using BLL.Models;
using BLL.Results;
using DAL.Entities;
using Microsoft.AspNetCore.Identity;

namespace BLL.Services
{
    public class AccountService : IAccountService
    {
        private readonly SignInManager<Person> _signInManager;
        private readonly UserManager<Person> _userManager;
        private readonly IMapper _mapper;
        private readonly ITokenGenerator _tokenGenerator;

        public AccountService(
            SignInManager<Person> signInManager,
            UserManager<Person> userManager,
            IMapper mapper,
            ITokenGenerator tokenGenerator)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _mapper = mapper;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<Result<string>> LoginAsync(LoginModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, true, false);
            if (!result.Succeeded)
            {
                return Result.Unauthorized<string>(BlErrors.WrongEmailOrPassword);
            }

            var person = await _userManager.FindByEmailAsync(model.Email);
            var token = await _tokenGenerator.GetToken(person);

            return token;
        }
    }
}