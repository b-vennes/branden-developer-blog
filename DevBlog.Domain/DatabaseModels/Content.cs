using System;

namespace DevBlog.Domain.DataBaseModels
{
    public class Content
    {        
        public string Id { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string ImageUrl { get; set; }
        public string Data { get; set; }
        public string Format { get; set; }
        public DateTime PublishedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool Hidden { get; set; }
    }
}