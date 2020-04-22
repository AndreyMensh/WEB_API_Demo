using Microsoft.EntityFrameworkCore;
using WEBAPI.Model.DatabaseModels;

namespace WEBAPI.Model
{
    public class ApplicationDatabaseContext : DbContext
    {
        public ApplicationDatabaseContext(DbContextOptions<ApplicationDatabaseContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(a => a.UserSettings)
                .WithOne(b => b.User)
                .HasForeignKey<UserSettings>(b => b.UserId);

            modelBuilder.Entity<Company>()
                .HasOne(a => a.CompanySettings)
                .WithOne(b => b.Company)
                .HasForeignKey<CompanySettings>(b => b.CompanyId);

            modelBuilder.Entity<Job>().HasOne(e => e.StartLocation)
                .WithMany(x => x.StartLocations).Metadata.DeleteBehavior = DeleteBehavior.Restrict;

            modelBuilder.Entity<User>().HasOne(e => e.Company)
                .WithMany(x => x.Users).Metadata.DeleteBehavior = DeleteBehavior.Restrict;


            modelBuilder.Entity<Job>().HasOne(e => e.EndLocation)
                .WithMany(x => x.EndLocations).Metadata.DeleteBehavior = DeleteBehavior.Restrict;

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<CompanySettings> CompanySettings { get; set; }
        public DbSet<UserSettings> UserSettings { get; set; }
        public DbSet<Act> Acts { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<TableSettings> TableSettings { get; set; }
        public DbSet<Break> Breaks { get; set; }
        public DbSet<Error> Errors { get; set; }
        public DbSet<AllowedUser> AllowedUsers { get; set; }
        public DbSet<TrustedIpAddress> TrustedIpAddresses { get; set; }
    }
}
