using Microsoft.AspNetCore.Mvc;
using carRental_WebAPI.Repositories;

namespace carRental_WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthRepository _repo;

        public AuthController(IAuthRepository repo)
        {
            _repo = repo;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserCredentials userCredentials)
        {
            if (_repo.Login(userCredentials.UserName, userCredentials.Password) == false) return BadRequest("The user name or password is incorrect.");

            return Ok(_repo.CreateToken(userCredentials.UserName));
        }
    }

    public class UserCredentials {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}