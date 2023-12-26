using Microsoft.EntityFrameworkCore;

namespace hastanerandevusistemi.Models
{
    public class ConnectionStringClass : DbContext
    {
        public ConnectionStringClass(DbContextOptions<ConnectionStringClass> options):base(options)
        {
            
        }
        public DbSet<HastaneClass> Hastaneler {  get; set; }
    }
}
