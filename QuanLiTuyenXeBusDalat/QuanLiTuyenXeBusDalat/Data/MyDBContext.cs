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
        public DbSet<TaiXe> TaiXes { get; set; }
        public DbSet<Xe> Xes { get; set; }
        public DbSet<TaiKhoan> taiKhoans { get; set; }

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
                e.HasIndex(e => e.UserName).IsUnique();
                e.Property(e => e.HoTen).IsRequired().HasMaxLength(150);
                e.Property(e => e.Email).IsRequired().HasMaxLength(150);
            });
        }
        #endregion
    }
}
