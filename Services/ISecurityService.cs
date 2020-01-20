using System.Threading.Tasks;

namespace DevBlog.Services
{
    public interface ISecurityService
    {
        Task<bool> ValidateToken(string token);
    }
}