using System;
using System.Threading.Tasks;
using DevBlog.Domain.Dtos;
using DevBlog.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace DevBlog.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContentController : ControllerBase
    {
        private readonly IContentService _contentService;

        public ContentController(IContentService contentService)
        {
            _contentService = contentService;
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
    }
}