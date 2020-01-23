using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevBlog.DatabaseModels;
using Microsoft.EntityFrameworkCore;

namespace DevBlog.Data
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

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}