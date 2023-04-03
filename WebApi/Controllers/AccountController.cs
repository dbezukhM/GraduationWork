using AutoMapper;
using BLL.Contracts;
using BLL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ResultController
    {
        private readonly IAccountService _accountService;

        public AccountController(
            IMapper mapper,
            IAccountService accountService)
            : base(mapper)
        {
            _accountService = accountService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(LoginRequest model)
        {
            var loginModel = Mapper.Map<LoginModel>(model);
            var result = await _accountService.LoginAsync(loginModel);

            return OperationResult<TokenModel, TokenResponse>(result);
        }

        [HttpPost("LogOut")]
        [Authorize]
        public async Task<ActionResult> LogOut()
        {
            await _accountService.LogOut();
            return Ok();
        }


        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var result = await _accountService.GetByIdAsync(id);

            return OperationResult<PersonGetModel, PersonGetResponse>(result);
        }

        [HttpGet("getByEmail/{email}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetByEmailAsync(string email)
        {
            var result = await _accountService.GetByEmailAsync(email);

            return OperationResult<PersonGetModel, PersonGetResponse>(result);
        }

        [HttpGet("getAuthorized")]
        [Authorize(Roles = "Admin, Lecturer, Methodist")]
        public async Task<IActionResult> GetAuthorizedAsync()
        {
            var result = await _accountService.GetByEmailAsync(User.FindFirst(ClaimTypes.Name)?.Value);

            return OperationResult<PersonGetModel, PersonGetResponse>(result);
        }

        [HttpGet("persons")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _accountService.GetAllAsync();

            return OperationResult<IEnumerable<PersonGetModel>, IEnumerable<PersonGetResponse>>(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateAsync(PersonCreateRequest model)
        {
            var personModel = Mapper.Map<PersonCreateModel>(model);
            var result = await _accountService.CreateAsync(personModel);

            return OperationResult(result);
        }

        [HttpPost("changePassword")]
        [Authorize]
        public async Task<IActionResult> ChangePasswordAsync(PersonChangePasswordRequest model)
        {
            var changePasswordModel = Mapper.Map<PersonChangePasswordModel>(model);
            changePasswordModel.Email = User.FindFirst(ClaimTypes.Name)?.Value;
            var result = await _accountService.ChangePasswordAsync(changePasswordModel);

            return OperationResult(result);
        }
    }
}