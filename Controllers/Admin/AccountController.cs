using System.Security.Claims;
using AgainPBL3.Data;
using AgainPBL3.Dtos.Account;
using AgainPBL3.Interfaces;
using AgainPBL3.Mapper;
using AgainPBL3.Models;
using AgainPBL3.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgainPBL3.Controllers.Admin
{
    
    [Route("api/admin/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAdminRepository _adminRepository;
        private readonly AccountService _accountService;
        private readonly JwtTokenProviderService _jwtTokenProviderService;
        public AccountController(AccountService accountService, JwtTokenProviderService jwtTokenProviderService, IAdminRepository adminRepository)
        {
            _accountService = accountService;
            _jwtTokenProviderService = jwtTokenProviderService;
            _adminRepository = adminRepository;
        }

        [HttpPost("signin")]
        public async Task<IActionResult> Signin([FromBody] SigninDto signinDto)
        {
            if (string.IsNullOrEmpty(signinDto.Password))
            {
                return BadRequest("Password is required.");
            }

            if (string.IsNullOrEmpty(signinDto.Email))
            {
                return BadRequest("Email is required.");
            }

            var user = await _adminRepository.GetUserByEmailAsync(signinDto.Email);
            if (user == null)
            {
                return NotFound("User not found");
            }

            if (_accountService.VerifyPassword(user.HashedPassword, signinDto.Password))
            {
                var token = _jwtTokenProviderService.GenerateToken(user.Name, user.UserID, user.RoleID);
                return Ok(new {token, user = user.MapToUserViewDto()});
            }

            return Unauthorized("Invalid password");
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var userId = User?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            await _adminRepository.UpdateLastLoginAsync(Convert.ToInt32(userId));
            return Ok(new { message = "Logged out successfully" }); 
        }

        [Authorize]
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            await _adminRepository.DeleteUserAsync(id);
            return Ok();
        }

        [Authorize]
        [HttpPost("ban/{id}")]
        public async Task<IActionResult> Ban([FromRoute] int id)
        {
            await _adminRepository.BanUserAsync(id);
            return Ok();
        }

        [Authorize]
        [HttpPost("undoban/{id}")]
        public async Task<IActionResult> UndoBan([FromRoute] int id)
        {
            await _adminRepository.UndoBanUserAsync(id);
            return Ok();
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser([FromRoute] int id)
        {
            //var userName = User?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
            //var userRole = User?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            //var userId = User?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var user = await _adminRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound("User not found");
            }

            return Ok(user.MapToUserViewDto());
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllUser()
        {
            var list = await _adminRepository.GetAllUserAsync();
            //nen them map
            return Ok(list.ToList());
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto createUserDto)
        {
            var hashedPassword = _accountService.HashPassword(createUserDto.Password);
            await _adminRepository.AddUserAsync(createUserDto.MapToUser(hashedPassword));

            return Ok(new { message = "Creating user successfully" });
        }

        [Authorize]
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserOfAdminDto updateUserOfAdminDto, [FromRoute] int id)
        {
            var user = updateUserOfAdminDto.MapToAdminUpdateQuery(id);
            await _adminRepository.UpdateUserAsync(user);
            return Ok();
        }

        [Authorize]
        [HttpGet("search")]
        public async Task<IActionResult> SearchUser([FromBody] SearchUserDto searchUserDto)
        {
            if (searchUserDto == null)
            {
                return BadRequest("Invalid search parameters.");
            }

            List<User> users = await _adminRepository.SearchUserAsync(searchUserDto.MapToUserSearchQuery());
            List<UserViewDto> userList= new List<UserViewDto>();
            if (users.Count == 0) return NotFound("Users not found!");
            else
            {
                foreach (var user in users)
                {
                    userList.Add(user.MapToUserViewDto());
                }
            }
            return Ok(userList.ToList());
        }

        [Authorize]
        [HttpPut("resetpassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto resetPasswordDto)
        {
            var userId = User?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var user = await _adminRepository.GetUserByIdAsync(Convert.ToInt32(userId));
            if (user == null)
            {
                return NotFound("User not found");
            }

            string hashedCurrentPassword = _accountService.HashPassword(resetPasswordDto.CurrentPassword);

            if (!_accountService.VerifyPassword(user.HashedPassword, hashedCurrentPassword))
            {
                return BadRequest("Password is incorrect");
            }
            await _adminRepository.ResetPasswordAsync(resetPasswordDto.NewPassword, Convert.ToInt32(userId));
            return Ok();
        }
    }
}
