using System.Linq;
using System.Threading.Tasks;
using DevBlog.Data;

namespace DevBlog.Services
{
    public class SecurityService : ISecurityService
    {
        private readonly ISecurityRepository _securityRepository;
        public SecurityService(ISecurityRepository securityRepository)
        {
            _securityRepository = securityRepository;
        }

        public async Task<bool> ValidateToken(string token)
        {
            var adminUsers = await _securityRepository.GetAll();

            return adminUsers.Any(u => u.Token.Equals(token));
        }
    }
}