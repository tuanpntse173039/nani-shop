using API.DTOs;
using API.Errors;
using API.Extensions;
using AutoMapper;
using Core.Entities.Identity;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AccountController(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            ITokenService tokenService,
            IMapper mapper
        )
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        private string GetAccessTokenFromHeader()
        {
            StringValues authorizationHeader = HttpContext.Request.Headers["Authorization"];
            string accessToken = string.Empty;
            if (authorizationHeader.ToString().StartsWith("Bearer"))
            {
                accessToken = authorizationHeader.ToString().Substring("Bearer ".Length).Trim();
            }
            return accessToken;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserDTO>> GetCurrentUser()
        {
            var user = await _userManager.FindUserByEmailClaimsPrincipalAsync(HttpContext.User);

            var accessToken = GetAccessTokenFromHeader();

            if (user == null || user.Email == null)
            {
                return Unauthorized(new ApiResponse(401));
            }

            return new UserDTO
            {
                Email = user.Email,
                DisplayName = user.DisplayName,
                Token = accessToken,
            };
        }

        [HttpGet("emailExists")]
        public async Task<ActionResult<bool>> CheckUserExist([FromQuery] string email)
        {
            return await _userManager.FindByEmailAsync(email) != null;
        }

        [Authorize]
        [HttpGet("address")]
        public async Task<ActionResult<AddressDTO>> GetUserAddress()
        {
            var user = await _userManager.FindUserByEmailClaimsPrincipalWithAddressAsync(
                HttpContext.User
            );

            if (user == null)
            {
                return Unauthorized(new ApiResponse(401));
            }

            return _mapper.Map<Address, AddressDTO>(user.Address);
        }

        [Authorize]
        [HttpPut("address")]
        public async Task<ActionResult<AddressDTO>> UpdateUserAddress(AddressDTO addressDTO)
        {
            var user = await _userManager.FindUserByEmailClaimsPrincipalWithAddressAsync(
                HttpContext.User
            );
            if (user == null)
            {
                return BadRequest(new ApiResponse(400));
            }
            var address = _mapper.Map<AddressDTO, Address>(addressDTO);
            user.Address = address;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                return BadRequest(new ApiResponse(400));
            }

            return Ok(_mapper.Map<Address, AddressDTO>(user.Address));
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
                Token = _tokenService.CreateToken(user)
            };
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO registerDTO)
        {
            if (CheckUserExist(registerDTO.Email).Result.Value)
            {
                return BadRequest(
                    new ApiValidationErrorResponse(new[] { "This email is already in use" })
                );
            }
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
                Token = _tokenService.CreateToken(appUser)
            };
        }
    }
}
