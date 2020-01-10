using System;

namespace Blog.Backend.Dtos
{
    public class ContentForDisplayDto
    {
        public string Name { get; set; }
        public string Data { get; set; }
        public DateTime PublishedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}