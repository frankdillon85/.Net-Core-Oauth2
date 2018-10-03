using Microsoft.EntityFrameworkCore;

namespace Oauth.Data.DB.Models
{
    public partial class JPVDBContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<WeeklyPoints> WeeklyPoints { get; set; }
        public virtual DbSet<WildCard> WildCards { get; set; }

        public JPVDBContext(DbContextOptions<JPVDBContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .ForSqlServerIsClustered(false);

                entity.HasIndex(e => e.Id)
                    .HasName("CIX_BallBags")
                    .IsUnique()
                    .ForSqlServerIsClustered();

                entity.Property(e => e.UserId).HasDefaultValueSql("(newsequentialid())");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Password).IsRequired();
            });

            modelBuilder.Entity<WeeklyPoints>(entity =>
            {
                entity.HasKey(e => e.WeeklyPointsId)
                    .ForSqlServerIsClustered(false);

                entity.HasIndex(e => e.Id)
                    .HasName("CIX_WeeklyPoints")
                    .IsUnique()
                    .ForSqlServerIsClustered();

                entity.Property(e => e.WeeklyPointsId).HasDefaultValueSql("(newsequentialid())");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Points).HasDefaultValueSql("((0))");

                entity.Property(e => e.WeekNumber).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<WildCard>(entity =>
            {
                entity.HasKey(e => e.WildcardId)
                    .ForSqlServerIsClustered(false);

                entity.HasIndex(e => e.Id)
                    .HasName("IX_WildCards")
                    .ForSqlServerIsClustered();

                entity.Property(e => e.WildcardId).HasDefaultValueSql("(newsequentialid())");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.WildcardName)
                    .IsRequired()
                    .HasMaxLength(50);
            });
        }
    }
}
