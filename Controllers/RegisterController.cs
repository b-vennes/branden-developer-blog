using System.Threading.Tasks;
using DevBlog.Dtos;
using DevBlog.Services;
using Microsoft.AspNetCore.Mvc;

namespace DevBlog.Controllers
{
    /*
    Keeping this section commented out unless I want to add a user.
    I'll think of a better solution to secure management later.

    [ApiController]
    [Route("api/[controller]")]
    public class RegisterController : ControllerBase
    {
        private readonly ISecurityService _securityService;
        public RegisterController(ISecurityService securityService)
        {
            _securityService = securityService;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser(RegisterUserDto registerUserDto)
        {
            if (!await _securityService.RegisterUser(registerUserDto.Token))
            {
                return BadRequest();
            }

            return NoContent();
        }
    }
    */
}