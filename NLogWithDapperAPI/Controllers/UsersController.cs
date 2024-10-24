using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLogWithDapperAPI.Models;
using NLogWithDapperAPI.Services;

namespace NLogWithDapperAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserRepository _userRepository;
        private readonly ILogger<UsersController> _logger;

        public UsersController(UserRepository userRepository, ILogger<UsersController> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            _logger.LogInformation("Fetching all users");
            var users = await _userRepository.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] User user)
        {
            _logger.LogInformation("Adding a new user");
            var result = await _userRepository.AddUserAsync(user);
            if (result > 0)
            {
                _logger.LogInformation("User added successfully");
                return Ok();
            }
            else
            {
                _logger.LogError("Error adding user");
                return BadRequest();
            }
        }
    }
}
