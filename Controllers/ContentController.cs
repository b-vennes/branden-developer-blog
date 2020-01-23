using System;
using System.Threading.Tasks;
using DevBlog.Dtos;
using DevBlog.Services;
using Microsoft.AspNetCore.Mvc;

namespace DevBlog.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContentController : ControllerBase
    {
        private readonly IContentService _contentService;
        private readonly ISecurityService _securityService;

        public ContentController(IContentService contentService, ISecurityService securityService)
        {
            _contentService = contentService;
            _securityService = securityService;
        }

        [HttpGet]
        public async Task<IActionResult> GetContentOverviews()
        {
            var contents = await _contentService.RetrieveContentOverviews();

            return Ok(contents);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetContentData(string id)
        {
            var contentData = await _contentService.RetrieveContentData(id);

            if (contentData == null)
            {
                return NotFound();
            }

            return Ok(contentData);
        }

        [HttpPost]
        public async Task<IActionResult> PublishContent(PublishContentDto contentToPublish)
        {
            var token = Request.Headers["Authorization"];
            if (!await _securityService.ValidateUser(token))
            {
                return Unauthorized();
            }

            var saveSuccess = false;

            try
            {
                saveSuccess = await _contentService.Publish(contentToPublish);
            }
            catch (ArgumentException argEx)
            {
                return BadRequest(argEx);
            }

            if (saveSuccess)
            {
                return NoContent();
            }
            else
            {
                return StatusCode(500, "Failed to publish content.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContent(string id, UpdateContentDto updateContentDto)
        {
            var token = Request.Headers["Authorization"];
            if (!await _securityService.ValidateUser(token))
            {
                return Unauthorized();
            }

            var saveSuccess = await _contentService.Update(id, updateContentDto);

            if (saveSuccess)
            {
                return NoContent();
            }
            else
            {
                return StatusCode(500, "Failed to update content.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArticle(string id)
        {
            var token = Request.Headers["Authorization"];
            if (!await _securityService.ValidateUser(token))
            {
                return Unauthorized();
            }

            var saveSuccess = false;

            try
            {
                saveSuccess = await _contentService.Delete(id);
            }
            catch (ArgumentException argEx)
            {
                return NotFound(argEx);
            }

            if (saveSuccess)
            {
                return NoContent();
            }
            else
            {
                return StatusCode(500, "Failed to delete content.");
            }
        }

    }
}