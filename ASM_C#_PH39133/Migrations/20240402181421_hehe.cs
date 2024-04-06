using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASM_C__PH39133.Migrations
{
    /// <inheritdoc />
    public partial class hehe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DanhMuc",
                columns: table => new
                {
                    IdDanhMuc = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenDanhMuc = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DanhMuc", x => x.IdDanhMuc);
                });

            migrationBuilder.CreateTable(
                name: "KhachHang",
                columns: table => new
                {
                    IdKH = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DienThoai = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    TenKH = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DiaChi = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    GioiTinh = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    MatKhau = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KhachHang", x => x.IdKH);
                });

            migrationBuilder.CreateTable(
                name: "SanPham",
                columns: table => new
                {
                    MaSP = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenSP = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IMEI = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    HinhAnh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    DonGia = table.Column<double>(type: "float", nullable: false),
                    Mota = table.Column<double>(type: "float", nullable: false),
                    IdDanhMuc = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SanPham", x => x.MaSP);
                    table.ForeignKey(
                        name: "FK_SanPham_DanhMuc_IdDanhMuc",
                        column: x => x.IdDanhMuc,
                        principalTable: "DanhMuc",
                        principalColumn: "IdDanhMuc",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GioHang",
                columns: table => new
                {
                    IdGioHang = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IDKH = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GioHang", x => x.IdGioHang);
                    table.ForeignKey(
                        name: "FK_GioHang_KhachHang_IDKH",
                        column: x => x.IDKH,
                        principalTable: "KhachHang",
                        principalColumn: "IdKH",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GioHangCT",
                columns: table => new
                {
                    IdGioHangCT = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdGioHang = table.Column<Guid>(type: "uniqueidentifier", maxLength: 100, nullable: false),
                    MaSP = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GioHangCT", x => x.IdGioHangCT);
                    table.ForeignKey(
                        name: "FK_GioHangCT_GioHang_MaSP",
                        column: x => x.MaSP,
                        principalTable: "GioHang",
                        principalColumn: "IdGioHang",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GioHangCT_SanPham_IdGioHang",
                        column: x => x.IdGioHang,
                        principalTable: "SanPham",
                        principalColumn: "MaSP",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GioHang_IDKH",
                table: "GioHang",
                column: "IDKH",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GioHangCT_IdGioHang",
                table: "GioHangCT",
                column: "IdGioHang");

            migrationBuilder.CreateIndex(
                name: "IX_GioHangCT_MaSP",
                table: "GioHangCT",
                column: "MaSP");

            migrationBuilder.CreateIndex(
                name: "IX_KhachHang_DienThoai",
                table: "KhachHang",
                column: "DienThoai",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SanPham_IdDanhMuc",
                table: "SanPham",
                column: "IdDanhMuc");

            migrationBuilder.CreateIndex(
                name: "IX_SanPham_IMEI",
                table: "SanPham",
                column: "IMEI",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GioHangCT");

            migrationBuilder.DropTable(
                name: "GioHang");

            migrationBuilder.DropTable(
                name: "SanPham");

            migrationBuilder.DropTable(
                name: "KhachHang");

            migrationBuilder.DropTable(
                name: "DanhMuc");
        }
    }
}
