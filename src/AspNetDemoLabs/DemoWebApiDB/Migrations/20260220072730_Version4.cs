using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DemoWebApiDB.Migrations
{
    /// <inheritdoc />
    public partial class Version4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CtgryId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Products_CtgryId",
                table: "Products",
                column: "CtgryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CtgryId",
                table: "Products",
                column: "CtgryId",
                principalSchema: "dbo",
                principalTable: "Categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CtgryId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_CtgryId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CtgryId",
                table: "Products");
        }
    }
}
