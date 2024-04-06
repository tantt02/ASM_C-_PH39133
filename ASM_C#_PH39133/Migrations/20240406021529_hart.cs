using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASM_C__PH39133.Migrations
{
    /// <inheritdoc />
    public partial class hart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SoLuong",
                table: "GioHangCT");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SoLuong",
                table: "GioHangCT",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
