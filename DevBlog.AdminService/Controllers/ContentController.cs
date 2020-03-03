using System.Threading.Tasks;
using DevBlog.Domain.Dtos;
using DevBlog.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace DevBlog.AdminService.Controllers
{
    [ApiController]
    [Route("api/admin/[controller]")]
    public class ContentController : ControllerBase
    {
        private readonly IContentService _contentService;
        private readonly string _passcode;

        public ContentController(IContentService contentService, IConfiguration config)
        {
            _contentService = contentService;
            _passcode = config.GetValue<string>("Passcode");
        }

        [HttpPost]
        public async Task<IActionResult> PublishContent(PublishContentDto contentForPublish, [FromHeader(Name="Authorization")] string passcode)
        {
            if (!_passcode.Equals(passcode))
            {
                return Unauthorized();
            }

            await _contentService.Publish(contentForPublish);

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContent(string id, UpdateContentDto contentForUpdate, [FromHeader(Name="Authorization")] string passcode)
        {
            if (!_passcode.Equals(passcode))
            {
                return Unauthorized();
            }

            await _contentService.Update(id, contentForUpdate);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContent(string id, [FromHeader(Name="Authorization")] string passcode)
        {
            if (!_passcode.Equals(passcode))
            {
                return Unauthorized();
            }

            await _contentService.Delete(id);

            return NoContent();
        }
    }
}