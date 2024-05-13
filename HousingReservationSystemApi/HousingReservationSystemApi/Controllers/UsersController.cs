using HousingReservationSystemApplication.Interfaces;
using HousingReservationSystemApplication.Services;
using Microsoft.AspNetCore.Mvc;

namespace HousingReservationSystemApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;
        private  readonly ILogger<UsersController> _logger;

        public UsersController(IUserService service, ILogger<UsersController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(string userName, string email, string password)
        {
            try
            {
                await _service.Register(userName, email, password);
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
                var token = await _service.Login(email, password);
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
