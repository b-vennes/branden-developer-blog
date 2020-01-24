using System.Linq;
using System.Text;
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

        public async Task<bool> RegisterUser(string token)
        {
            var newUser = new User();
            byte[] tokenHash, tokenSalt;
            CreatePasswordHash(token, out tokenHash, out tokenSalt);

            newUser.TokenHash = tokenHash;
            newUser.TokenSalt = tokenSalt;

            _securityRepository.AddUser(newUser);
            return await _securityRepository.SaveAll();
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

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}