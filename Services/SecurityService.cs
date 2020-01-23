using System.Linq;
using System.Threading.Tasks;
using DevBlog.Data;
using DevBlog.DatabaseModels;
using Microsoft.Extensions.Configuration;

namespace DevBlog.Services
{
    public class SecurityService : ISecurityService
    {
        private readonly ISecurityRepository _securityRepository;
        public SecurityService(ISecurityRepository securityRepository, IConfiguration config)
        {
            _securityRepository = securityRepository;
        }

        public async Task<bool> ValidateUser(string token)
        {
            var users = await _securityRepository.GetAllUsers();

            return users.Any(u => VerifyToken(token, u.TokenHash, u.TokenSalt));
        }

        private bool VerifyToken(string token, byte[] tokenHash, byte[] tokenSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(tokenSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(token));

                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != tokenHash[i]) return false;
                }
            }

            return true;
        }
    }
}