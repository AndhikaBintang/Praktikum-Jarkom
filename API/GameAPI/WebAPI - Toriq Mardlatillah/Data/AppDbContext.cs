using GameApi.Models;
using Microsoft.EntityFrameworkCore;

namespace GameApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<PlayerScore> PlayerScores => Set<PlayerScore>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PlayerScore>(entity =>
            {
                entity.Property(p => p.PlayerName)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(p => p.Score)
                      .IsRequired();

                // Biarkan DB yang mengisi CreatedAt → tidak ada nilai dinamis di HasData
                entity.Property(p => p.CreatedAt)
                      .HasDefaultValueSql("CURRENT_TIMESTAMP"); // SQLite

                // Index untuk leaderboard (sort by Score, tie-breaker Id)
                entity.HasIndex(p => new { p.Score, p.Id });
            });

            // Seeding data: TIDAK mengisi CreatedAt / GUID / nilai dinamis lainnya
            modelBuilder.Entity<PlayerScore>().HasData(
    new PlayerScore
    {
        Id = 1,
        PlayerName = "Andi",
        Score = 1200,
        CreatedAt = new DateTime(2025, 11, 5, 0, 0, 0, DateTimeKind.Utc)
    },
    new PlayerScore
    {
        Id = 2,
        PlayerName = "Nadia",
        Score = 980,
        CreatedAt = new DateTime(2025, 11, 5, 0, 0, 0, DateTimeKind.Utc)
    }
);

        }
    }
}
