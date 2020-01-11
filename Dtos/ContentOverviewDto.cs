using System;

namespace Blog.Backend.Dtos
{
    public class ContentOverviewDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public DateTime PublishedDate { get; set; }
    }
}