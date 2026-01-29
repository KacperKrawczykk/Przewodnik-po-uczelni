using Microsoft.EntityFrameworkCore;
using NowyPrzewodnikMVC.Models;

namespace NowyPrzewodnikMVC.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Tabele w bazie danych
        public DbSet<Waypoint> Waypoints { get; set; }
        public DbSet<Connection> Connections { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Konfiguracja relacji - żeby baza wiedziała jak łączyć strzałki z miejscami
            modelBuilder.Entity<Connection>()
                .HasOne(c => c.Source)
                .WithMany(w => w.OutboundConnections)
                .HasForeignKey(c => c.SourceId)
                .OnDelete(DeleteBehavior.Restrict); // Zabezpieczenie przed kaskadowym usuwaniem
        }
    }
}