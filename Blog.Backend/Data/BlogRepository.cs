using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Blog.Backend.Data
{
    public class BlogRepository : IBlogRepository
    {
        private readonly DataContext _context;
        public BlogRepository(DataContext context)
        {
            _context = context;

        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<IEnumerable<Article>> GetArticles()
        {
            return await _context.Articles.ToListAsync();
        }

        public async Task<Article> GetArticle(int id)
        {
            return await _context.Articles
                .Where(a => a.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}