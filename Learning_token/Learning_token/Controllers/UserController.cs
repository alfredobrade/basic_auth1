using Learning_token.Models.Custom;
using Learning_token.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Learning_token.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAuthService _authService;

        public UserController(IAuthService authService)
        {
            _authService = authService;
        }



        [HttpPost]
        [Route("Authenticate")]
        public async Task<IActionResult> Autenticar([FromBody] AuthRequest authentication)
        {
            var authentication_result = await _authService.GetToken(authentication);
            if (authentication_result == null) return Unauthorized();



            return Ok(authentication_result);
        }
    }
}
