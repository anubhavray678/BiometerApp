using BiometerApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BiometerApp.Data
{
    public class BlogDetailsDbContext:DbContext
    {
        public BlogDetailsDbContext(DbContextOptions<BlogDetailsDbContext> options) : base(options) 
        
        {
            
        }
        public DbSet<BlogDetails> BlogData { get; set; }
    }

}
