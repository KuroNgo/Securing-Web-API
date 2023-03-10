using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLiTuyenXeBusDalat.Migrations
{
    /// <inheritdoc />
    public partial class AddTaiXe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Xe",
                columns: table => new
                {
                    MaXe = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BienSo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LoaiXe = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoGhe = table.Column<int>(type: "int", nullable: false),
                    CongSuat = table.Column<float>(type: "real", nullable: false),
                    ChuKyBaoHanh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgaySX = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Xe", x => x.MaXe);
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
                name: "IX_TaiXe_MaXe",
                table: "TaiXe",
                column: "MaXe");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaiXe");

            migrationBuilder.DropTable(
                name: "Xe");
        }
    }
}
