using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.APIs.Errors;
using OrderManagementSystem.Core.DataTransferObjects;
using OrderManagementSystem.Core.Services;

namespace OrderManagementSystem.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto model)
        {
            var result = await _userService.RegisterUserAsync(model);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto model)
        {
            var result = await _userService.LoginUserAsync(model);
            return result is not null? Ok(result):Unauthorized(new ApiResponse(401, "Incorrect Email or Password"));
        }

    }
}
