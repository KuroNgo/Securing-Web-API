using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLiTuyenXeBusDalat.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DonViQLXe",
                columns: table => new
                {
                    MaDonVi = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenDonVi = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DiaChi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoDienThoai = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonViQLXe", x => x.MaDonVi);
                });

            migrationBuilder.CreateTable(
                name: "TaiKhoan",
                columns: table => new
                {
                    MaTaiKhoan = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    HoTen = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    NgayThangNamSinh = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    SDT = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaiKhoan", x => x.MaTaiKhoan);
                });

            migrationBuilder.CreateTable(
                name: "Tuyen",
                columns: table => new
                {
                    MaTuyen = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaDonVi = table.Column<int>(type: "int", nullable: false),
                    TenTuyen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ThoiGianBatDau = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ThoiGianKetThuc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ThoiGianGianCach = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LoTrinhLuotDi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LoTrinhLuotVe = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LoaiTuyen = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tuyen", x => x.MaTuyen);
                    table.ForeignKey(
                        name: "FK_Tuyen_DonViQLXe_MaDonVi",
                        column: x => x.MaDonVi,
                        principalTable: "DonViQLXe",
                        principalColumn: "MaDonVi",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Xe",
                columns: table => new
                {
                    MaXe = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BienSo = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    LoaiXe = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoGhe = table.Column<int>(type: "int", nullable: false),
                    CongSuat = table.Column<float>(type: "real", nullable: false),
                    ChuKyBaoHanh = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    NgaySX = table.Column<DateTime>(type: "datetime2", maxLength: 150, nullable: false),
                    MaTuyen = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Xe", x => x.MaXe);
                    table.ForeignKey(
                        name: "FK_Xe_Tuyen_MaTuyen",
                        column: x => x.MaTuyen,
                        principalTable: "Tuyen",
                        principalColumn: "MaTuyen",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaiXe",
                columns: table => new
                {
                    MaTX = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HoVaTen = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NgaySinh = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()"),
                    GioiTinh = table.Column<bool>(type: "bit", nullable: false),
                    DiaChi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QueQuan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgayBDHopDong = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Luong = table.Column<double>(type: "float", nullable: false),
                    BangLai = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaXe = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaiXe", x => x.MaTX);
                    table.ForeignKey(
                        name: "FK_TaiXe_Xe_MaXe",
                        column: x => x.MaXe,
                        principalTable: "Xe",
                        principalColumn: "MaXe",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaiKhoan_MaTaiKhoan",
                table: "TaiKhoan",
                column: "MaTaiKhoan",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaiXe_MaXe",
                table: "TaiXe",
                column: "MaXe");

            migrationBuilder.CreateIndex(
                name: "IX_Tuyen_MaDonVi",
                table: "Tuyen",
                column: "MaDonVi");

            migrationBuilder.CreateIndex(
                name: "IX_Xe_MaTuyen",
                table: "Xe",
                column: "MaTuyen");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaiKhoan");

            migrationBuilder.DropTable(
                name: "TaiXe");

            migrationBuilder.DropTable(
                name: "Xe");

            migrationBuilder.DropTable(
                name: "Tuyen");

            migrationBuilder.DropTable(
                name: "DonViQLXe");
        }
    }
}
