using BiometerApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BiometerApp.Data
{
    public class ContactDbContext : DbContext 
    {
        public ContactDbContext(DbContextOptions<ContactDbContext> options) : base(options) 
        {
        
        }
        public DbSet<Contact> ContactData { get; set; }
    }
}
