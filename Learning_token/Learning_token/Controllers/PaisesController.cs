using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Learning_token.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaisesController : ControllerBase
    {
        [Authorize]
        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var listaPaises = await Task.FromResult(new List<string>
            { "francia", "argentina", "croacia", "marruecos"});


            return Ok(listaPaises);
        }
    }
}
