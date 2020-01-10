namespace Blog.Backend.Dtos
{
    public class UpdateContentDto
    {
        public string Url { get; set; }
        public string Format { get; set; }
        public bool Hidden { get; set; }
    }
}