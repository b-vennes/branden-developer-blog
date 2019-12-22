using System.Collections.Generic;
using System.Threading.Tasks;
using Blog.Backend.Models;

namespace Blog.Backend.Data
{
    public interface IBlogRepository
    {
        void Add<T>(T entity) where T: class;

        void Delete<T>(T entity) where T: class;
        Task<bool> SaveAll();
        Task<IEnumerable<Article>> GetArticles();
        Task<Article> GetArticle(int id);
    }
}