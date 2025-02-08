using IMDBClone.Models;
using Microsoft.EntityFrameworkCore;

namespace IMDBClone.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<UserRoles> UserRoles { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Director> Directors { get; set; }
        public DbSet<Producer> Producers { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Series> Serieses { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<SeriesGenres> SeriesGenres { get; set; }
        public DbSet<MovieGenres> MovieGenres { get; set; }
        public DbSet<SeriesRatings> SeriesRatings { get; set; }
        public DbSet<MovieRatings> MovieRatings { get; set; }
        public DbSet<Episode> Episodes { get; set; }
        public DbSet<Season> Seasons { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
                
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserRoles>().HasKey(x => new { x.UserId, x.RoleId });
            modelBuilder.Entity<SeriesGenres>().HasKey(x => new { x.SeriesId, x.GenreId });
            modelBuilder.Entity<MovieGenres>().HasKey(x => new { x.MovieId, x.GenreId });
            modelBuilder.Entity<SeriesRatings>().HasKey(x => new { x.SeriesId, x.UserId });
            modelBuilder.Entity<MovieRatings>().HasKey(x => new { x.MovieId, x.UserId });
            modelBuilder.Entity<MovieActors>().HasKey(x => new { x.MovieId, x.ActorId });
            modelBuilder.Entity<SeriesActors>().HasKey(x => new { x.SeriesId, x.ActorId });
        }
    }
}
