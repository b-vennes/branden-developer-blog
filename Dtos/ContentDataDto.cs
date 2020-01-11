using System;

namespace Blog.Backend.Dtos
{
    public class ContentDataDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Data { get; set; }
        public DateTime PublishedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}