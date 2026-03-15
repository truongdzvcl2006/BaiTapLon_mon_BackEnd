using Microsoft.EntityFrameworkCore;
using RealEstateAuction.Models;

namespace RealEstateAuction.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<PropertyDetail> PropertyDetails { get; set; }
        public DbSet<PropertyImage> PropertyImages { get; set; }
        public DbSet<PropertyAmenity> PropertyAmenities { get; set; }
        public DbSet<Auction> Auctions { get; set; }
        public DbSet<Bid> Bids { get; set; }
        public DbSet<Deposit> Deposits { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Cấu hình cho bảng Users
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.Email).IsUnique();
                entity.HasIndex(e => e.Username).IsUnique(false);
                entity.Property(e => e.RoleId).HasDefaultValue(2);
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETDATE()");
                entity.Property(e => e.IsActive).HasDefaultValue(true);
            });

            // Cấu hình cho Property
            modelBuilder.Entity<Property>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Price).HasPrecision(18, 2);
                entity.Property(e => e.CreatedDate).HasDefaultValueSql("GETDATE()");
            });

            // Cấu hình cho PropertyDetail
            modelBuilder.Entity<PropertyDetail>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Price).HasPrecision(18, 2);

                // SỬA LẠI: .Scascade -> .SetNull
                entity.HasOne(p => p.User)
                    .WithMany()  // User có thể có nhiều PropertyDetail?
                    .HasForeignKey(p => p.UserId)
                    .OnDelete(DeleteBehavior.SetNull);  // Sửa từ Scascade thành SetNull
            });

            // Cấu hình quan hệ giữa Bid và Auction
            modelBuilder.Entity<Bid>()
                .HasOne(b => b.Auction)
                .WithMany(a => a.Bids)
                .HasForeignKey(b => b.AuctionId)
                .OnDelete(DeleteBehavior.Cascade);

            // Cấu hình quan hệ giữa Bid và User
            modelBuilder.Entity<Bid>()
                .HasOne(b => b.User)
                .WithMany(u => u.Bids)
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Cấu hình cho Auction
            modelBuilder.Entity<Auction>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.StartingPrice).HasPrecision(18, 2);
                entity.Property(e => e.CurrentPrice).HasPrecision(18, 2);
                entity.Property(e => e.ReservePrice).HasPrecision(18, 2);

                entity.HasOne(a => a.Property)
                    .WithMany()
                    .HasForeignKey(a => a.PropertyId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Cấu hình cho Bid
            modelBuilder.Entity<Bid>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Amount).HasPrecision(18, 2);
                entity.Property(e => e.BidTime).HasDefaultValueSql("GETDATE()");
            });

            // Cấu hình cho Deposit
            modelBuilder.Entity<Deposit>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Amount).HasPrecision(18, 2);
                entity.Property(e => e.DepositDate).HasDefaultValueSql("GETDATE()");

                entity.HasOne(d => d.User)
                    .WithMany(u => u.Deposits)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.Auction)
                    .WithMany()
                    .HasForeignKey(d => d.AuctionId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}