using System;
using System.Threading.Tasks;
using Blog.Backend.Dtos;
using Blog.Backend.Managers;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContentController : ControllerBase
    {
        private readonly IContentManager _contentManager;

        public ContentController(IContentManager contentManager)
        {
            _contentManager = contentManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetContentOverviews()
        {
            var contents = await _contentManager.RetrieveContentOverviews();

            return Ok(contents);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetContentData(string id)
        {
            var contentData = await _contentManager.RetrieveContentData(id);

            if (contentData == null)
            {
                return NotFound();
            }

            return Ok(contentData);
        }

        [HttpPost]
        public async Task<IActionResult> PublishContent(PublishContentDto contentToPublish)
        {
            var saveSuccess = false;

            try
            {
                saveSuccess = await _contentManager.Publish(contentToPublish);
            }
            catch(ArgumentException argEx)
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
            var saveSuccess = await _contentManager.Update(id, updateContentDto);

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
            var saveSuccess = false; 
            
            try
            {
                saveSuccess = await _contentManager.Delete(id);
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