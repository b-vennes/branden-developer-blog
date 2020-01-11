using System.Collections.Generic;
using System.Threading.Tasks;
using Blog.Backend.DatabaseModels;

namespace Blog.Backend.Data
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