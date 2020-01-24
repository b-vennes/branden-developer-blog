using System.Collections.Generic;
using System.Threading.Tasks;
using DevBlog.Domain.DataBaseModels;

namespace DevBlog.Domain.Data
{
    public interface IContentRepository
    {
        void Add(Content entity);

        void Delete(Content entity);
        Task<bool> SaveAll();
        Task<List<Content>> GetAll();
        Task<Content> Get(string id);
    }
}