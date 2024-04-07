using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASM_C__PH39133.Migrations
{
    /// <inheritdoc />
    public partial class ab : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GioHangCT_GioHang_MaSP",
                table: "GioHangCT");

            migrationBuilder.DropForeignKey(
                name: "FK_GioHangCT_SanPham_IdGioHang",
                table: "GioHangCT");

            migrationBuilder.AddForeignKey(
                name: "FK_GioHangCT_GioHang_IdGioHang",
                table: "GioHangCT",
                column: "IdGioHang",
                principalTable: "GioHang",
                principalColumn: "IdGioHang",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GioHangCT_SanPham_MaSP",
                table: "GioHangCT",
                column: "MaSP",
                principalTable: "SanPham",
                principalColumn: "MaSP",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GioHangCT_GioHang_IdGioHang",
                table: "GioHangCT");

            migrationBuilder.DropForeignKey(
                name: "FK_GioHangCT_SanPham_MaSP",
                table: "GioHangCT");

            migrationBuilder.AddForeignKey(
                name: "FK_GioHangCT_GioHang_MaSP",
                table: "GioHangCT",
                column: "MaSP",
                principalTable: "GioHang",
                principalColumn: "IdGioHang",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GioHangCT_SanPham_IdGioHang",
                table: "GioHangCT",
                column: "IdGioHang",
                principalTable: "SanPham",
                principalColumn: "MaSP",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
