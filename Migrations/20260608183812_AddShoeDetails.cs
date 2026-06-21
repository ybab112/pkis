using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyWebApp.Migrations
{
    /// <inheritdoc />
    public partial class AddShoeDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "Shoes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Material",
                table: "Shoes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Season",
                table: "Shoes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Shoes");

            migrationBuilder.DropColumn(
                name: "Material",
                table: "Shoes");

            migrationBuilder.DropColumn(
                name: "Season",
                table: "Shoes");
        }
    }
}
