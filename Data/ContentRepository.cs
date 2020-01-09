using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Blog.Backend.Data
{
    public class ContentRepository : IContentRepository
    {
        private readonly DataContext _context;
        public ContentRepository(DataContext context)
        {
            _context = context;

        }

        public void Add(Content entity)
        {
            _context.Add(entity);
        }

        public void Delete(Content entity)
        {
            _context.Remove(entity);
        }

        public async Task<IEnumerable<Content>> GetAll()
        {
            return await _context.Contents.ToListAsync();
        }

        public async Task<Content> Get(string id)
        {
            return await _context.Contents
                .Where(a => a.Id.Equals(id))
                .FirstOrDefaultAsync();
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}