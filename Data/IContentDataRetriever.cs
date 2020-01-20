namespace DevBlog.Data
{
    public interface IContentDataRetriever
    {
        string GetData(string url, string format);
    }
}