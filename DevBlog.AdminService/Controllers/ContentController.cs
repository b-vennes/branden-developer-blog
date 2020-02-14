using System;
using System.Threading.Tasks;
using DevBlog.Domain.Dtos;
using DevBlog.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace DevBlog.AdminService.Controllers
{
    [ApiController]
    [Route("api/admin/[controller]")]
    public class ContentController : ControllerBase
    {
        private readonly IContentService _contentService;

        public ContentController(IContentService contentService)
        {
            _contentService = contentService;
        }

        [HttpPost]
        public async Task<IActionResult> PublishContent(PublishContentDto contentForPublish)
        {
            await _contentService.Publish(contentForPublish);

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContent(string id, UpdateContentDto contentForUpdate)
        {
            await _contentService.Update(id, contentForUpdate);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContent(string id)
        {
            await _contentService.Delete(id);

            return NoContent();
        }
    }
}