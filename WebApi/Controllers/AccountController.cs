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
        [Authorize(Roles = "Admin, Lecturer")]
        public async Task<ActionResult> LogOut()
        {
            await _accountService.LogOut();
            return Ok();
        }


        [HttpGet("id")]
        [Authorize(Roles = "Admin, Lecturer")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var result = await _accountService.GetByIdAsync(id);

            return OperationResult<PersonGetModel, PersonGetResponse>(result);
        }

        [HttpPost]
        //[Authorize(Roles = "Admin")]
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