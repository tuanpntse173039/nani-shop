using API.DTOs;
using API.Errors;
using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager
        )
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDTO)
        {
            var user = await _userManager.FindByEmailAsync(loginDTO.Email);

            if (user == null || user.Email == null)
                return Unauthorized(new ApiResponse(401));

            var result = await _signInManager.CheckPasswordSignInAsync(
                user,
                loginDTO.Password,
                false
            );

            if (!result.Succeeded)
                return Unauthorized(new ApiResponse(401));

            return new UserDTO
            {
                Email = user.Email,
                DisplayName = user.DisplayName,
                Token = "This will be a token"
            };
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO registerDTO)
        {
            var appUser = new AppUser
            {
                Email = registerDTO.Email,
                DisplayName = registerDTO.DisplayName,
                UserName = registerDTO.Email
            };
            var result = await _userManager.CreateAsync(appUser, registerDTO.Password);

            if (!result.Succeeded)
                return BadRequest(new ApiResponse(400));


            return new UserDTO
            {
                Email = appUser.Email,
                DisplayName = appUser.DisplayName,
                Token = "Thiss"
            };
        }
    }
}
