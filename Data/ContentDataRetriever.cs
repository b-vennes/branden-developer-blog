using System.Net;
using System.Text.RegularExpressions;
using Markdig;
using RestSharp;

namespace DevBlog.Data
{
    public class ContentDataRetriever : IContentDataRetriever
    {
        private readonly IRestClient _restClient;

        public ContentDataRetriever(IRestClient restClient)
        {
            _restClient = restClient;
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
                _ => GetDataFromMarkdwon(response.Content)
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