using Microsoft.EntityFrameworkCore;
using NowyPrzewodnikMVC.Models;

namespace NowyPrzewodnikMVC.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Waypoint> Waypoints { get; set; }
        public DbSet<Connection> Connections { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Konfiguracja relacji
            modelBuilder.Entity<Connection>()
                .HasOne(c => c.Source)
                .WithMany(w => w.OutboundConnections)
                .HasForeignKey(c => c.SourceId)
                .OnDelete(DeleteBehavior.Restrict); // Ważne dla bezpieczeństwa

            modelBuilder.Entity<Connection>()
                .HasOne(c => c.Target)
                .WithMany()
                .HasForeignKey(c => c.TargetId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}