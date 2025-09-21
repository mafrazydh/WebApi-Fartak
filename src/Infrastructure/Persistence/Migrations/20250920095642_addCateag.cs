using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addCateag : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Categories_CategoryId",
                schema: "FartakNew",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "Category",
                schema: "FartakNew",
                table: "Product");

            migrationBuilder.AlterColumn<Guid>(
                name: "CategoryId",
                schema: "FartakNew",
                table: "Product",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Categories_CategoryId",
                schema: "FartakNew",
                table: "Product");

            migrationBuilder.AlterColumn<Guid>(
                name: "CategoryId",
                schema: "FartakNew",
                table: "Product",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                schema: "FartakNew",
                table: "Product",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Categories_CategoryId",
                schema: "FartakNew",
                table: "Product",
                column: "CategoryId",
                principalSchema: "FartakNew",
                principalTable: "Categories",
                principalColumn: "Id");
        }
    }
}
