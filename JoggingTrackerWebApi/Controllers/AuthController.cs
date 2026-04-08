using JoggingTrackerWebApi.Dto;
using JoggingTrackerWebApi.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JoggingTrackerWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync([FromBody]RegisterDto dto)
        {
           var result= await _authService.RegisterAsync(dto);

            if (result != "User registered successfully")
            {
                return BadRequest(result);
            }
            
            return Ok(result);

        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync([FromBody]LoginDto dto)
        {
            var result= await _authService.LoginAsync(dto);
           
            //in case of error message
            if (result == "Invalid username or password")
                return Unauthorized(new {error = result});
            
            //in case of token
            return Ok(new {token = result});
        }
    }
}
