using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevBlog.Domain.DataBaseModels;
using Microsoft.EntityFrameworkCore;

namespace DevBlog.Domain.Data
{
    public class SecurityRepository : ISecurityRepository
    {
        private readonly DataContext _context;

        public SecurityRepository(DataContext context)
        {
            _context = context;
        }

        public Task<List<User>> GetAllUsers()
        {
            return _context.Users.ToListAsync();
        }

        public void AddUser(User user)
        {
            _context.Users.Add(user);
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}