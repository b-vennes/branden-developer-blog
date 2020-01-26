using System.Collections.Generic;
using System.Threading.Tasks;
using DevBlog.Domain.Dtos;

namespace DevBlog.Domain.Services
{
    public interface IContentService
    {
        /// <summary>
        /// Retrieves the overviews of all non-hidden content.
        /// </summary>
        /// <returns>The overviews of all non-hidden contents.</returns>
        Task<List<ContentOverviewDto>> RetrieveContentOverviews();

        /// <summary>
        /// Retrives the content data for the content with given id. 
        /// </summary>
        /// <param name="id">The id of the content.</param>
        /// <returns>The content's data.</returns>
        Task<ContentDataDto> RetrieveContentData(string id);

        /// <summary>
        /// Publishes the content.
        /// </summary>
        /// <param name="publishContent">The content to publish.</param>
        /// <returns>The success of the save action.</returns>
        Task<bool> Publish(PublishContentDto publishContent);

        /// <summary>
        /// Updates the content.
        /// </summary>
        /// <param name="id">The id of the content.</param>
        /// <param name="updateContent">The updated content.</param>
        /// <returns>The success of the save action.</returns>
        Task<bool> Update(string id, UpdateContentDto updateContent);

        /// <summary>
        /// Deletes the content.
        /// </summary>
        /// <param name="id">The id of the content.</param>
        /// <returns>The success of the save action.</returns>
        Task<bool> Delete(string id);
    }
}