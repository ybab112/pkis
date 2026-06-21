using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyWebApp.Migrations
{
    /// <inheritdoc />
    public partial class ReplaceSizeWithShoeSizes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Size",
                table: "Shoes");

            migrationBuilder.CreateTable(
                name: "ShoeSize",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Size = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    ShoeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoeSize", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShoeSize_Shoes_ShoeId",
                        column: x => x.ShoeId,
                        principalTable: "Shoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShoeSize_ShoeId",
                table: "ShoeSize",
                column: "ShoeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShoeSize");

            migrationBuilder.AddColumn<int>(
                name: "Size",
                table: "Shoes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
