using HousingReservationSystemApplication.Services;
using Microsoft.AspNetCore.Mvc;

namespace HousingReservationSystemApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;
        private  readonly ILogger<UsersController> _logger;

        public UsersController(UserService userService, ILogger<UsersController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(string userName, string email, string password)
        {
            try
            {
                await _userService.Register(userName, email, password);
                _logger.LogInformation("User registered successfully");
                return Ok("User registered successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Registration failed");
                return BadRequest($"Registration failed: {ex.Message}");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(string email, string password)
        {
            try
            {
                var token = await _userService.Login(email, password);
                _logger.LogInformation("User logged in successfully");
                return Ok(token);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Login failed");
                return BadRequest($"Login failed: {ex.Message}");
            }
        }
    }
}
