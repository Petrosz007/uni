using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TodoList.Persistence;
using TodoList.Persistence.DTO;

namespace TodoList.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
        }

        // api/Account/Login
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDto user)
        {
            if (_signInManager.IsSignedIn(User))
                await _signInManager.SignOutAsync();

            var result = await _signInManager.PasswordSignInAsync(user.UserName, user.Password, isPersistent: false,
                lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return Ok();
            }

            return Unauthorized("Login failed!");
        }

        // api/Account/Logout
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return Ok();
        }
    }
}