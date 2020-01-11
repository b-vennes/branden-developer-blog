namespace Blog.Backend.Data
{
    public interface IContentDataRetriever
    {
        string GetData(string url, string format);
    }
}