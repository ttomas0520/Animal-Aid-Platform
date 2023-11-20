using AnimalAidPlatform.API.Models;
using AnimalAidPlatform.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace AnimalAidPlatform.API.Data
{

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<FeedPost> FeedPosts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Animal> Animals { get; set; }
        public DbSet<AnimalShelter> AnimalShelters { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AnimalShelter>()
                .HasMany(e => e.Admins)
                .WithOne(e => e.AdminWorkingPlace)
                .HasForeignKey(e => e.AnimalShelterId)
                .HasPrincipalKey(e => e.Id);

            modelBuilder.Entity<AnimalShelter>()
                .HasMany(e => e.Workers)
                .WithOne(e => e.RegularWorkingPlace)
                .HasForeignKey(e => e.AnimalShelterId)
                .HasPrincipalKey(e => e.Id);
        }
    }
}
