namespace DevBlog.Domain.Data
{
    public interface IContentDataRetriever
    {
        string GetData(string url, string format);
    }
}