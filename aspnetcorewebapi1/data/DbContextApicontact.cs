using aspnetcorewebapi1.Model;
using Microsoft.EntityFrameworkCore;

namespace aspnetcorewebapi1.data
{
    public class DbContextApicontact : DbContext
    {
        public DbContextApicontact(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Contact> Contacts { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            
        }
    }
}