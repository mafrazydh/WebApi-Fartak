using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                schema: "FartakNew",
                table: "Product");

            migrationBuilder.AddColumn<Guid>(
                name: "CategoryId",
                schema: "FartakNew",
                table: "Product",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "CategoryName",
                schema: "FartakNew",
                table: "Product",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Categories",
                schema: "FartakNew",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryDescription = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Product_CategoryId",
                schema: "FartakNew",
                table: "Product",
                column: "CategoryId");

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

            migrationBuilder.DropTable(
                name: "Categories",
                schema: "FartakNew");

            migrationBuilder.DropIndex(
                name: "IX_Product_CategoryId",
                schema: "FartakNew",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                schema: "FartakNew",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "CategoryName",
                schema: "FartakNew",
                table: "Product");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                schema: "FartakNew",
                table: "Product",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
