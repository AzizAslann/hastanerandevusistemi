using Microsoft.EntityFrameworkCore;

namespace hastanerandevusistemi.Models
{
    public class ConnectionStringClass : DbContext
    {
        public ConnectionStringClass(DbContextOptions<ConnectionStringClass> options):base(options)
        {         
        }
        public DbSet<Doktorlar> Doktorlars { get; set; }
        public DbSet<RandevuAl> randevuAls { get; set; }  
    }
}
