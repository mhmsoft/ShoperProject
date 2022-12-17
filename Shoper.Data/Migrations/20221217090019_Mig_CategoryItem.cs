using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shoper.Data.Migrations
{
    /// <inheritdoc />
    public partial class MigCategoryItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "Fk_ProductItemToProduct",
                table: "ProductItem");

            migrationBuilder.DropColumn(
                name: "ItemValue",
                table: "ProductItem");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "ProductItem",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductItem_ProductId",
                table: "ProductItem",
                newName: "IX_ProductItem_CategoryId");

            migrationBuilder.CreateTable(
                name: "ProductItemValue",
                columns: table => new
                {
                    ItemValueId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductItemValue", x => x.ItemValueId);
                    table.ForeignKey(
                        name: "Fk_ProductItemValueToProduct",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "Fk_ProductItemValueToProductItem",
                        column: x => x.ItemId,
                        principalTable: "ProductItem",
                        principalColumn: "ItemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductItemValue_ItemId",
                table: "ProductItemValue",
                column: "ItemId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductItemValue_ProductId",
                table: "ProductItemValue",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "Fk_ProductItemToCategory",
                table: "ProductItem",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "Fk_ProductItemToCategory",
                table: "ProductItem");

            migrationBuilder.DropTable(
                name: "ProductItemValue");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "ProductItem",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductItem_CategoryId",
                table: "ProductItem",
                newName: "IX_ProductItem_ProductId");

            migrationBuilder.AddColumn<string>(
                name: "ItemValue",
                table: "ProductItem",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "Fk_ProductItemToProduct",
                table: "ProductItem",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
