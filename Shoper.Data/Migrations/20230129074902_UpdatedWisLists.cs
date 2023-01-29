using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shoper.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedWisLists : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WishList_Product_productId",
                table: "WishList");

            migrationBuilder.AddForeignKey(
                name: "Fk_WishListsToProduct",
                table: "WishList",
                column: "productId",
                principalTable: "Product",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "Fk_WishListsToProduct",
                table: "WishList");

            migrationBuilder.AddForeignKey(
                name: "FK_WishList_Product_productId",
                table: "WishList",
                column: "productId",
                principalTable: "Product",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
