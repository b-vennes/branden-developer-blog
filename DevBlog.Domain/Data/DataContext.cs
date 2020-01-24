using DevBlog.Domain.DataBaseModels;
using Microsoft.EntityFrameworkCore;

namespace DevBlog.Domain.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Content> Contents { get; set; }
        public DbSet<User> Users { get; set; }
    }
}