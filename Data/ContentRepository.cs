using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Blog.Backend.Models;
using Markdig;
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

        public string GetData(string url, string format)
        {
            var request = new RestRequest(url);
            var response = _restClient.Get(request);

            if (response == null || response.StatusCode != HttpStatusCode.OK)
            {
                return null;
            }

            var data = format switch {
                "markdown" => GetDataFromMarkdwon(response.Content),
                _ => null
            };

            return data;
        }

        private string GetDataFromMarkdwon(string markdownText)
        {
            var html = Markdown.ToHtml(markdownText);

            // remove newline characters
            html = Regex.Replace(html, @"\t|\n|\r", "");

            return html;
        }
    }
}