using AutoMapper;
using BLL.Contracts;
using BLL.Errors;
using BLL.Models;
using BLL.Results;
using BLL.Settings;
using DAL.DatabaseInitializers;
using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace BLL.Services
{
    public class AccountService : IAccountService
    {
        private readonly SignInManager<Person> _signInManager;
        private readonly UserManager<Person> _userManager;
        private readonly ProgramSettings _settings;
        private readonly IMapper _mapper;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly IPasswordService _passwordService;
        private readonly IEmailSender _emailSender;

        public AccountService(
            SignInManager<Person> signInManager,
            UserManager<Person> userManager,
            IOptionsSnapshot<ProgramSettings> settings,
            IMapper mapper,
            ITokenGenerator tokenGenerator,
            IPasswordService passwordService,
            IEmailSender emailSender)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _settings = settings.Value;
            _mapper = mapper;
            _tokenGenerator = tokenGenerator;
            _passwordService = passwordService;
            _emailSender = emailSender;
        }

        public async Task<Result<TokenModel>> LoginAsync(LoginModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, true, false);
            if (!result.Succeeded)
            {
                return Result.Unauthorized<TokenModel>(BlErrors.WrongEmailOrPassword);
            }

            var person = await _userManager.FindByEmailAsync(model.Email);
            var token = await _tokenGenerator.GetToken(person);
            var tokenModel = new TokenModel
            {
                Token = token.Value,
            };

            return Result.Success(tokenModel);
        }

        public async Task<Result> LogOut()
        {
            await _signInManager.SignOutAsync();
            return Result.Success();
        }

        public async Task<Result<Guid>> CreateAsync(PersonCreateModel model)
        {
            var person = _mapper.Map<Person>(model);
            person.IsFirstPasswordChanged = false;
            person.IsAdmin = model.Roles.Contains(IdentityInitializer.AdminRoleName);
            person.UserName = model.Email;
            var password = _passwordService.GeneratePassword();
            var result = await _userManager.CreateAsync(person, password);

            if (!result.Succeeded)
            {
                return Result.ValidationError<Guid>(_mapper.Map<IEnumerable<Error>>(result.Errors));
            }

            foreach (var role in model.Roles)
            {
                await _userManager.AddToRoleAsync(person, role);
            }

            var welcomeEmailModel = CreateWelcomeEmailModel(person.Email, password);
            await _emailSender.SendEmailAsync(welcomeEmailModel);

            return Result.Success(person.Id);
        }

        public async Task<Result> ChangePasswordAsync(PersonChangePasswordModel model)
        {
            var person = await _userManager.FindByEmailAsync(model.Email);
            if (person == null)
            {
                return Result.NotFound(BlErrors.EntityNotFound);
            }

            var correctOldPassword = await _userManager.CheckPasswordAsync(person, model.OldPassword);
            if (!correctOldPassword)
            {
                return Result.ValidationError(BlErrors.PasswordNotCorrect);
            }

            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(person);
            var result = await _userManager.ResetPasswordAsync(person, resetToken, model.NewPassword);

            if (!result.Succeeded)
            {
                return Result.ValidationError(BlErrors.PasswordIsSimple);
            }

            if (!person.IsFirstPasswordChanged)
            {
                person.IsFirstPasswordChanged = true;
                await _userManager.UpdateAsync(person);
            }

            return Result.Success();
        }

        public async Task<Result<PersonGetModel>> GetByIdAsync(Guid personId)
        {
            var person = await _userManager.Users.Include(u => u.WorkingProgramsAuthor)
                .Include(u => u.WorkingProgramsApprover)
                .FirstOrDefaultAsync(u => u.Id == personId);
            if (person == null)
            {
                return Result.NotFound<PersonGetModel>(BlErrors.NotFound(personId));
            }

            var result = _mapper.Map<PersonGetModel>(person);

            return Result.Success(result);
        }

        public async Task<Result<PersonGetModel>> GetByEmailAsync(string email)
        {
            var person = await _userManager.Users.Include(u => u.WorkingProgramsAuthor)
                .Include(u => u.WorkingProgramsApprover)
                .FirstOrDefaultAsync(u => u.Email == email);
            if (person == null)
            {
                return Result.NotFound<PersonGetModel>(BlErrors.EntityNotFound);
            }

            var result = _mapper.Map<PersonGetModel>(person);

            return Result.Success(result);
        }

        public async Task<Result<IEnumerable<PersonGetModel>>> GetAllAsync()
        {
            var persons = await _userManager.Users.ToListAsync();
            var result = _mapper.Map<IEnumerable<PersonGetModel>>(persons);

            return Result.Success(result);
        }

        private SendEmailModel CreateWelcomeEmailModel(string email, string password)
        {
            var result = new SendEmailModel
            {
                To = email,
                Subject = "Створення акаунту",
                EmailBody = $"Вітаю!<br/>Для Вас було створено акаунт для входу на платформу \"Робочі дисципліни університетів\".<br/>Посилання - {_settings.ClientUrl}/login<br/>Логін - <b>{email}</b><br/>Пароль - <b>{password}</b>",
            };

            return result;
        }
    }
}