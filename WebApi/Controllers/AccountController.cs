using AutoMapper;
using BLL.Contracts;
using BLL.Models;
using Microsoft.AspNetCore.Mvc;
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

            return OperationResult(result);
        }
    }
}