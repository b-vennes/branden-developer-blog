using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevBlog.Domain.DataBaseModels;
using MongoDB.Driver;
using RestSharp;

namespace DevBlog.Domain.Data
{
    public class ContentRepository : IContentRepository
    {
        private readonly IRestClient _restClient;

        private readonly IMongoCollection<Content> _content;

        public ContentRepository(IRestClient restClient, IMongoClient mongoClient)
        {
            _restClient = restClient;
            var database = mongoClient.GetDatabase("devblog");

            _content = database.GetCollection<Content>("content");
        }

        public void Add(Content content)
        {
            _content.InsertOneAsync(content);
        }

        public void Update(Content entity)
        {
            _content.ReplaceOne(c => c.Id == entity.Id, entity);
        }

        public void Delete(Content entity)
        {
            _content.DeleteOne(c => c.Id == entity.Id);
        }

        public async Task<List<Content>> GetAll()
        {
            return await _content.Find(c => true).ToListAsync();
        }

        public async Task<Content> Get(string id)
        {
            return await _content.Find(c => c.Id == id).FirstOrDefaultAsync();
        }
    }
}