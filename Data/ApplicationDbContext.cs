using HierarchicalItemProcessingSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace HierarchicalItemProcessingSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Item> Items { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Self-referencing relationship config
            modelBuilder.Entity<Item>()
                .HasOne(i => i.Parent)
                .WithMany(i => i.Children)
                .HasForeignKey(i => i.ParentId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascading deletes to handle dependencies properly

            modelBuilder.Entity<Item>()
                .Property(i => i.Weight)
                .HasPrecision(18, 2);

            // Seed test user
            modelBuilder.Entity<User>().HasData(new User 
            {
                Id = 1,
                Email = "test@user.com",
                PasswordHash = "pass"
            });

            // Seed test input items
            modelBuilder.Entity<Item>().HasData(
                new Item { Id = 1, Name = "Raw Steel Block", Weight = 150.00m, Status = "Unprocessed", CreatedAt = DateTime.UtcNow },
                new Item { Id = 2, Name = "Wooden Log", Weight = 45.50m, Status = "Unprocessed", CreatedAt = DateTime.UtcNow }
            );
        }
    }
}
