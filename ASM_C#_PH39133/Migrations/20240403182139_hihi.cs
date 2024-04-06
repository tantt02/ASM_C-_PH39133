using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASM_C__PH39133.Migrations
{
    /// <inheritdoc />
    public partial class hihi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Mota",
                table: "SanPham",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddColumn<bool>(
                name: "IsAdmin",
                table: "KhachHang",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAdmin",
                table: "KhachHang");

            migrationBuilder.AlterColumn<double>(
                name: "Mota",
                table: "SanPham",
                type: "float",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
