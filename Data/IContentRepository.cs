using System.Collections.Generic;
using System.Threading.Tasks;
using Blog.Backend.Models;

namespace Blog.Backend.Data
{
    public interface IContentRepository
    {
        void Add(Content entity);

        void Delete(Content entity);
        Task<bool> SaveAll();
        Task<IEnumerable<Content>> GetAll();
        Task<Content> Get(string id);
        string GetData(string url, string format);
    }
}