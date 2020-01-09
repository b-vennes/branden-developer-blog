using System;
using System.Linq;
using System.Threading.Tasks;
using Blog.Backend.Data;
using Blog.Backend.Dtos;
using Blog.Backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArticlesController : ControllerBase
    {
        private readonly IContentRepository _contentRepository;

        public ArticlesController(IContentRepository contentRepository)
        {
            _contentRepository = contentRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllContents()
        {
            var contents = await _contentRepository.GetAll();

            return Ok(contents);
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetContent(string name)
        {
            var content = await _contentRepository.Get(name);

            if (content == null)
            {
                return NotFound();
            }

            return Ok(content);
        }

        [HttpPost]
        public async Task<IActionResult> PublishContent(PublishContentDto contentToPublish)
        {
            var contents = await _contentRepository.GetAll();

            if (contents.Any(c => c.Id == contentToPublish.Name))
            {
                return BadRequest($"Content with name {contentToPublish.Name} already exists.");
            }

            var content = new Content() {
                Url = contentToPublish.Url,
                Hidden = contentToPublish.Hidden,
                PublishedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            };

            _contentRepository.Add(content);

            if (await _contentRepository.SaveAll())
            {
                return NoContent();
            }

            throw new Exception($"Publishing content failed on save");
        }

        [HttpPut("{name}")]
        public async Task<IActionResult> UpdateContent(string name, UpdateContentDto updateContentDto)
        {
            var article = await _contentRepository.Get(name);

            article.Url = updateContentDto.Url;
            article.Hidden = updateContentDto.Hidden;
            article.UpdatedDate = DateTime.Now;

            if (await _contentRepository.SaveAll())
            {
                return NoContent();
            }

            throw new Exception($"Updating content failed on save");
        }

        [HttpDelete("{name}")]
        public async Task<IActionResult> DeleteArticle(string name)
        {
            var article = await _contentRepository.Get(name);

            if (article == null)
            {
                return NotFound();
            }

            _contentRepository.Delete(article);

            if (await _contentRepository.SaveAll())
            {
                return NoContent();
            }

            throw new Exception("Deleting content failed on save");
        }

    }
}