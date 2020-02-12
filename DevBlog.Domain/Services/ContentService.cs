using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevBlog.Domain.Data;
using DevBlog.Domain.DataBaseModels;
using DevBlog.Domain.Dtos;
using Microsoft.Extensions.Logging;

namespace DevBlog.Domain.Services
{
    public class ContentService : IContentService
    {
        private readonly ILogger<ContentService> _logger;
        private readonly IContentRepository _contentRepository;
        private readonly IContentDataRetriever _contentDataRetriever;

        private readonly string[] nonOverviewContent = { "about", "resume" };

        public ContentService(ILogger<ContentService> logger, IContentRepository contentRepository, IContentDataRetriever contentDataRetriever)
        {
            _logger = logger;
            _contentRepository = contentRepository;
            _contentDataRetriever = contentDataRetriever;
        }

        public async Task Delete(string id)
        {
            var content = await _contentRepository.Get(id);

            if (content == null)
            {
                var errorMessage = $"Content with id {id} was not found.";
                _logger.LogError(errorMessage);
                throw new ArgumentException(errorMessage);
            }

            _contentRepository.Delete(content);

            _logger.LogInformation($"Successfully deleted content with id {content.Id}");
        }

        public async Task Publish(PublishContentDto publishContent)
        {
            var contents = await _contentRepository.GetAll();

            if (contents.Any(c => c.Id == publishContent.Id))
            {
                var errorMessage = $"Content with id {publishContent.Id} already exists.";
                _logger.LogError(errorMessage);
                throw new ArgumentException(errorMessage);
            }

            var data = _contentDataRetriever.GetData(publishContent.Url, publishContent.Format);

            var content = new Content()
            {
                Id = publishContent.Id,
                Title = publishContent.Title,
                Summary = publishContent.Summary,
                ImageUrl = publishContent.ImageUrl,
                Data = data,
                Hidden = publishContent.Hidden,
                Format = publishContent.Format,
                PublishedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            };

            _contentRepository.Add(content);

            _logger.LogInformation($"Successfully added content with id {content.Id}");
        }

        public async Task<ContentDataDto> RetrieveContentData(string id)
        {
            var content = await _contentRepository.Get(id);

            if (content == null)
            {
                return null;
            }

            var contentForDisplay = new ContentDataDto()
            {
                Id = content.Id,
                Title = content.Title,
                Data = content.Data,
                Summary = content.Summary,
                ImageUrl = content.ImageUrl,
                PublishedDate = content.PublishedDate,
                UpdatedDate = content.UpdatedDate
            };

            return contentForDisplay;
        }

        public async Task<List<ContentOverviewDto>> RetrieveContentOverviews()
        {
            var contents = await _contentRepository.GetAll();

            var contentOverviews = contents.Where(c => !c.Hidden && !nonOverviewContent.Contains(c.Id))
                .Select(c =>
                {
                    return new ContentOverviewDto()
                    {
                        Id = c.Id,
                        Title = c.Title,
                        Summary = c.Summary,
                        ImageUrl = c.ImageUrl,
                        PublishedDate = c.PublishedDate,
                        UpdatedDate = c.UpdatedDate
                    };
                })
                .OrderByDescending(c => c.PublishedDate)
                .ToList();

            return contentOverviews;
        }

        public async Task Update(string id, UpdateContentDto updateContent)
        {
            var contentToUpdate = await _contentRepository.Get(id);

            var updatedData = _contentDataRetriever.GetData(updateContent.Url, updateContent.Format);

            contentToUpdate.Title = updateContent.Title;
            contentToUpdate.Data = updatedData;
            contentToUpdate.Summary = updateContent.Summary;
            contentToUpdate.ImageUrl = updateContent.ImageUrl;
            contentToUpdate.Format = updateContent.Format;
            contentToUpdate.Hidden = updateContent.Hidden;
            contentToUpdate.UpdatedDate = DateTime.Now;

            _contentRepository.Update(contentToUpdate);

            _logger.LogInformation($"Successfully updated content with id {id}");
        }
    }
}