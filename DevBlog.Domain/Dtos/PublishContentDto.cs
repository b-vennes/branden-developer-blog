namespace DevBlog.Domain.Dtos
{
    public class PublishContentDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string Summary { get; set; }
        public string ImageUrl { get; set; }
        public string Format { get; set; }
        public bool Hidden { get; set; }
    }
}