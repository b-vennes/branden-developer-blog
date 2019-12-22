using System;
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
        private readonly IBlogRepository _repo;

        public ArticlesController(IBlogRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetArticles()
        {
            var articles = await _repo.GetArticles();

            return Ok(articles);
        }

        [HttpPost]
        public async Task<IActionResult> PublishArticle(PublishArticleDto articleToPublish)
        {
            var article = new Article() {
                Url = articleToPublish.Url,
                Hidden = articleToPublish.Hidden,
                PublishedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            };

            _repo.Add(article);

            if (await _repo.SaveAll())
            {
                return NoContent();
            }

            throw new Exception($"Publishing article failed on save");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateArticle(int id, UpdateArticleDto updatedArticle)
        {
            var article = await _repo.GetArticle(id);

            article.Url = updatedArticle.Url;
            article.Hidden = updatedArticle.Hidden;
            article.UpdatedDate = DateTime.Now;

            if (await _repo.SaveAll())
            {
                return NoContent();
            }

            throw new Exception($"Updating article failed on save");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArticle(int id)
        {
            var article = await _repo.GetArticle(id);

            if (article == null)
            {
                return NotFound();
            }

            _repo.Delete(article);

            if (await _repo.SaveAll())
            {
                return NoContent();
            }

            throw new Exception("Deleting article failed on save");
        }

    }
}