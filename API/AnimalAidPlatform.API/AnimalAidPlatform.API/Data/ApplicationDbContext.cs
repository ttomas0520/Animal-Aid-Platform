using AnimalAidPlatform.API.Models;
using AnimalAidPlatform.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace AnimalAidPlatform.API.Data
{

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<FeedPost> FeedPosts { get; set; }
        public DbSet<FeedPostLike> FeedPostLikes { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Animal> Animals { get; set; }
        public DbSet<AnimalShelter> AnimalShelters { get; set; }
        public DbSet<NotificationSettings> NotificationSettings { get; set; }
        public DbSet<Notification> Notifications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<NotificationSettings>()
                .HasOne(e => e.User)
                .WithOne(e => e.NotificationSettings)
                .HasForeignKey<NotificationSettings>(ns => ns.UserId);

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

            modelBuilder.Entity<FeedPost>()
                .HasOne(e => e.Category)
                .WithMany(e => e.Posts)
                .HasForeignKey(e => e.CategoryId)
                .HasPrincipalKey(e => e.Id);

            modelBuilder.Entity<FeedPost>()
                .HasOne(e => e.Creator)
                .WithMany(e => e.Posts)
                .HasForeignKey(e => e.CreatorId)
                .HasPrincipalKey(e => e.Id);

            modelBuilder.Entity<FeedPost>()
            .Property(f => f.Location)
            .HasColumnType("geography");

            modelBuilder.Entity<NotificationSettings>()
                .Property(n => n.Location)
                .HasColumnType("geography");

            modelBuilder.Entity<FeedPostLike>()
            .HasKey(pl => new { pl.FeedPostId, pl.UserId }); 

            modelBuilder.Entity<FeedPostLike>()
                .HasOne(pl => pl.FeedPost)
                .WithMany(fp => fp.PostLikes)
                .HasForeignKey(pl => pl.FeedPostId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<FeedPostLike>()
                .HasOne(pl => pl.User)
                .WithMany()
                .HasForeignKey(pl => pl.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
