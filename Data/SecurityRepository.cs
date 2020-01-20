using System.Collections.Generic;
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

        public async Task<List<User>> GetAll()
        {
            return await _context.AdminUsers.ToListAsync();
        }
    }
}