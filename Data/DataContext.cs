using DevBlog.DatabaseModels;
using Microsoft.EntityFrameworkCore;

namespace DevBlog.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Content> Contents { get; set; }
        public DbSet<User> Users { get; set; }
    }
}