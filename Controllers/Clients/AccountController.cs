using AgainPBL3.Dtos.Account;
using AgainPBL3.Interfaces;
using AgainPBL3.Mapper;
using AgainPBL3.Models;
using AgainPBL3.Repository.UserRepo;
using AgainPBL3.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HeThongMoiGioiDoCu.Controllers.Clients
{

    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IClientRepository _clientRepository;
        private readonly AccountService _accountService;
        private readonly JwtTokenProviderService _jwtTokenProviderService;

        public AccountController(AccountService accountService, JwtTokenProviderService jwtTokenProviderService, IClientRepository clientRepository)
        {
            _accountService = accountService;
            _jwtTokenProviderService = jwtTokenProviderService;
            _clientRepository = clientRepository;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> Signup([FromBody] SignupDto signupDto)
        {
            try
            {
                var existingEmail = await _clientRepository.GetUserByEmailAsync(signupDto.Email);
                if (existingEmail != null)
                {
                    return Conflict("User with this email already exists.");
                }
            }
            catch (InvalidOperationException ex)
            {                
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = "error", message = "An error occurred during signup" });
            }

            var hashedPassword = _accountService.HashPassword(signupDto.Password);

            var user = signupDto.MapToUser(hashedPassword);
            try
            {
                await _clientRepository.AddUserAsync(user);

                return Ok(user.MapToUserViewDto());
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = "error", message = "An error occurred during signup" });
            }

        }

        [HttpPost("signin")]
        public async Task<IActionResult> Signin([FromBody] SigninDto signinDto)
        {
            if (string.IsNullOrWhiteSpace(signinDto.Email))
            {
                return BadRequest("Email is required!");
            }

            if (string.IsNullOrWhiteSpace(signinDto.Password))
            {
                return BadRequest("Password is required!");
            }

            var user = await _clientRepository.GetUserByEmailAsync(signinDto.Email);

            if (user == null || user.RoleID == 1 || user.RoleID == 4)
            {
                return NotFound("User not found!");
            }

            if (_accountService.VerifyPassword(user.HashedPassword, signinDto.Password))
            {
                var token = _jwtTokenProviderService.GenerateToken(user.Name, user.UserID, user.RoleID);
                return Ok(new { token, user = user.MapToUserViewDto() });
            }

            return Unauthorized("Invalid password!");
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var userId = User?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            
            if (string.IsNullOrEmpty(userId) || !int.TryParse(userId, out var id))
            {
                return Unauthorized(new { status = "error", message = "User not authenticated" });
            }

            try
            {
                await _clientRepository.UpdateLastLoginAsync(id);
                return Ok(new { status = "success", message = "Logged out successfully" });
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { status = "error", message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = "error", message = "An error occurred during logout" });
            }
        }

        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            var userId = User?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var user = await _clientRepository.GetUserByIdAsync(Convert.ToInt32(userId));
            if (user == null)
            {
                return NotFound("User not found");
            }

            if (user.Status == "Inactive")
            {
                return BadRequest("User not actived");
            }

            return Ok(user.MapToUserViewDto());
        }

        //[Authorize]
        [HttpPut("update")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDto updateUserDto)
        {
            var userId = User?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var user = updateUserDto.MapToClientUpdateQuery(Convert.ToInt32(userId));
            await _clientRepository.UpdateUserAsync(user);
            var u = await _clientRepository.GetUserByIdAsync(Convert.ToInt32(userId));
            return Ok(u);
        }

        //[Authorize]
        [HttpGet("search")]
        public async Task<IActionResult> SearchUser([FromQuery] string keyword) {
                      
            var users = await _clientRepository.SearchUserAsync(keyword);
            if (users.Count == 0 || users == null) return NotFound("No users found matching the given keyword.");
            var userList = users.Select(u => u.MapToUserViewDto()).ToList();
            return Ok(userList);
        }

        //[Authorize]
        [HttpPost("registerseller")]
        public async Task<IActionResult> RegisterSeller()
        {
            var userId = User?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            await _clientRepository.RegisterSellerAsync(Convert.ToInt32(userId));
            return Ok();
        }

        //[Authorize]
        [HttpPut("resetpassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto resetPasswordDto)
        {
            var userId = User?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var user = await _clientRepository.GetUserByIdAsync(Convert.ToInt32(userId));
            if (user == null)
            {
                return NotFound("User not found");
            }

            string hashedCurrentPassword = _accountService.HashPassword(resetPasswordDto.CurrentPassword);

            if (!_accountService.VerifyPassword(user.HashedPassword, hashedCurrentPassword))
            {
                return BadRequest("Password is incorrect");
            }
            await _clientRepository.ResetPasswordAsync(resetPasswordDto.NewPassword, Convert.ToInt32(userId));
            return Ok(user);
        }
    }
}
