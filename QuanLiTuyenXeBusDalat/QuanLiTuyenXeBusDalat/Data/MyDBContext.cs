using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace QuanLiTuyenXeBusDalat.Data
{
    public class MyDBContext : DbContext
    {
        public MyDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }
        #region DbSet
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<TaiXe> TaiXes { get; set; }
        public DbSet<Xe> Xes { get; set; }
        public DbSet<TaiKhoan> taiKhoans { get; set; }
        public DbSet<Tuyen> tuyens { get; set; }
        public DbSet<DonViQuanLiXe> donViQuanLiXes { get; set; }

        // định nghĩa Fluent API
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Biên dịch lênh add migration
            modelBuilder.Entity<TaiXe>(e =>
            {
                e.ToTable("TaiXe");

                // Khai báo khóa chính
                e.HasKey(tx => tx.MaTX);

                // UTC là lấy múi giờ thế giới
                e.Property(tx => tx.NgaySinh)
                    .HasDefaultValueSql("getutcdate()");
            });

            // Biên dịch lệnh add-migration 
            modelBuilder.Entity<TaiKhoan>(e =>
            {
                e.HasIndex(e => e.MaTaiKhoan).IsUnique();
                e.Property(e => e.HoTen).IsRequired().HasMaxLength(150);
                e.Property(e => e.Email).IsRequired().HasMaxLength(150);
            });

            // BIeen dich lenh add migration
            modelBuilder.Entity<Xe>(e =>
            {
                e.ToTable("Xe");

                // Khai báo khóa chính
                e.HasKey(e => e.MaXe);
                e.Property(e => e.BienSo).IsRequired().HasMaxLength(150);
                e.Property(e=>e.ChuKyBaoHanh).IsRequired().HasMaxLength(150);
                e.Property(e => e.NgaySX).IsRequired().HasMaxLength(150);
            });

            modelBuilder.Entity<DonViQuanLiXe>(e =>
            {
                e.ToTable("DonViQLXe");

                // Khai báo khóa chính
                e.HasKey(e => e.MaDonVi);
                e.Property(e => e.SoDienThoai).IsRequired().HasMaxLength(20);
                e.Property(e => e.TenDonVi).IsRequired().HasMaxLength(200);
            });
        }
        #endregion
    }
}
