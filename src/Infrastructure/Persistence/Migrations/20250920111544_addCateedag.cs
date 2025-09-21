using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addCateedag : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Categories_CategoryId",
                schema: "FartakNew",
                table: "Product");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Categories_CategoryId",
                schema: "FartakNew",
                table: "Product",
                column: "CategoryId",
                principalSchema: "FartakNew",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Categories_CategoryId",
                schema: "FartakNew",
                table: "Product");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Categories_CategoryId",
                schema: "FartakNew",
                table: "Product",
                column: "CategoryId",
                principalSchema: "FartakNew",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
