using Microsoft.EntityFrameworkCore;
using RapidGames.Models;


namespace RapidGames.Data 
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<CategoryGames> CategoryGames { get; set; }
        public DbSet<Review> Review { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CategoryGames>()
                .HasKey(cg => cg.CategoryGamesId);

            modelBuilder.Entity<CategoryGames>()
                .HasOne(cg => cg.Category)
                .WithMany(c => c.CategoryGames)
                .HasForeignKey(cg => cg.CategoryId);

            modelBuilder.Entity<CategoryGames>()
                .HasOne(cg => cg.Game)
                .WithMany(g => g.CategoryGames)
                .HasForeignKey(cg => cg.GameId);
        }
    }
}