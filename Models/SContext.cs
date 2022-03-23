using Microsoft.EntityFrameworkCore;
namespace Models{
    public class SContext:DbContext{
         public SContext(DbContextOptions options):base(options)
        {
            
        }
        public DbSet<Stanica> Stanice { get; set; }
        public DbSet<Destinacija> Destinacije { get; set; }
        public DbSet<Putnik> Putnici { get; set; }
        public DbSet<Putovanje> Putovanja { get; set; }
        public DbSet<Termin> Termini { get; set; }
    }
}