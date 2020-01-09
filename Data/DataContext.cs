using Blog.Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Blog.Backend.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Content> Contents { get; set; }
    }
}