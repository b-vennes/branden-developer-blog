using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Backend.Data;
using Blog.Backend.DatabaseModels;
using Blog.Backend.Dtos;

namespace Blog.Backend.Managers
{
    public class ContentManager : IContentManager
    {
        private readonly IContentRepository _contentRepository;
        private readonly IContentDataRetriever _contentDataRetriever;

        public ContentManager(IContentRepository contentRepository, IContentDataRetriever contentDataRetriever)
        {
            _contentRepository = contentRepository;
            _contentDataRetriever = contentDataRetriever;

        }

        public async Task<bool> Delete(string id)
        {
            var content = await _contentRepository.Get(id);

            if (content == null)
            {
                throw new ArgumentException($"Content with id {id} was not found.");
            }

            _contentRepository.Delete(content);

            return await _contentRepository.SaveAll();
        }

        public async Task<bool> Publish(PublishContentDto publishContent)
        {
            var contents = await _contentRepository.GetAll();

            if (contents.Any(c => c.Id == publishContent.Id))
            {
                throw new ArgumentException($"Content with id {publishContent.Id} already exists.");
            }

            var content = new Content()
            {
                Id = publishContent.Id,
                Title = publishContent.Title,
                Url = publishContent.Url,
                Hidden = publishContent.Hidden,
                PublishedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            };

            _contentRepository.Add(content);

            return await _contentRepository.SaveAll();
        }

        public async Task<ContentDataDto> RetrieveContentData(string id)
        {
            var content = await _contentRepository.Get(id);

            if (content == null)
            {
                return null;
            }

            var data = _contentDataRetriever.GetData(content.Url, content.Format);

            var contentForDisplay = new ContentDataDto()
            {
                Id = content.Id,
                Title = content.Title,
                Data = data,
                PublishedDate = content.PublishedDate,
                UpdatedDate = content.UpdatedDate
            };

            return contentForDisplay;
        }

        public async Task<List<ContentOverviewDto>> RetrieveContentOverviews()
        {
            var contents = await _contentRepository.GetAll();

            var contentOverviews = contents.Where(c => !c.Hidden)
                .Select(c =>
                {
                    return new ContentOverviewDto()
                    {
                        Id = c.Id,
                        Title = c.Title,
                        PublishedDate = c.PublishedDate
                    };
                }).ToList();

            return contentOverviews;
        }

        public async Task<bool> Update(string id, UpdateContentDto updateContent)
        {
            var contentToUpdate = await _contentRepository.Get(id);

            contentToUpdate.Url = updateContent.Url;
            contentToUpdate.Title = updateContent.Title;
            contentToUpdate.Format = updateContent.Format;
            contentToUpdate.Hidden = updateContent.Hidden;
            contentToUpdate.UpdatedDate = DateTime.Now;

            return await _contentRepository.SaveAll();
        }
    }
}