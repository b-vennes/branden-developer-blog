using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Backend.DatabaseModels;
using Microsoft.EntityFrameworkCore;
using RestSharp;

namespace Blog.Backend.Data
{
    public class ContentRepository : IContentRepository
    {
        private readonly DataContext _context;
        private readonly IRestClient _restClient;

        public ContentRepository(DataContext context, IRestClient restClient)
        {
            _context = context;
            _restClient = restClient;
        }

        public void Add(Content entity)
        {
            _context.Add(entity);
        }

        public void Delete(Content entity)
        {
            _context.Remove(entity);
        }

        public async Task<List<Content>> GetAll()
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