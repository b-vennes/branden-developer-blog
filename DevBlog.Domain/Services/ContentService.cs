using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevBlog.Domain.Data;
using DevBlog.Domain.DataBaseModels;
using DevBlog.Domain.Dtos;

namespace DevBlog.Domain.Services
{
    public class ContentService : IContentService
    {
        private readonly IContentRepository _contentRepository;
        private readonly IContentDataRetriever _contentDataRetriever;

        private readonly string[] nonOverviewContent = { "about", "resume" };

        public ContentService(IContentRepository contentRepository, IContentDataRetriever contentDataRetriever)
        {
            _contentRepository = contentRepository;
            _contentDataRetriever = contentDataRetriever;
        }

        public async Task Delete(string id)
        {
            var content = await _contentRepository.Get(id);

            if (content == null)
            {
                throw new ArgumentException($"Content with id {id} was not found.");
            }

            _contentRepository.Delete(content);
        }

        public async Task Publish(PublishContentDto publishContent)
        {
            var contents = await _contentRepository.GetAll();

            if (contents.Any(c => c.Id == publishContent.Id))
            {
                throw new ArgumentException($"Content with id {publishContent.Id} already exists.");
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
                }).ToList();

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
        }
    }
}