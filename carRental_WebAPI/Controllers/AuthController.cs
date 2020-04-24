using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
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

        /// <summary>
        /// Validate the provided user credentials
        /// </summary>
        /// <param name="userCredentials"></param>
        /// <returns>A JSON Web Token</returns>
        /// <response code="200">Returns the JWT</response>
        /// <response code="400">If the credentials are not valid</response>
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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