using System;

namespace DevBlog.Domain.Dtos
{
    public class ContentOverviewDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string ImageUrl { get; set; }
        public DateTime PublishedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}