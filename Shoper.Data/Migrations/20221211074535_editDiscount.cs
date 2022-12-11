using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shoper.Data.Migrations
{
    /// <inheritdoc />
    public partial class editDiscount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiscountCode",
                table: "ProductDiscount");

            migrationBuilder.AlterColumn<bool>(
                name: "Status",
                table: "ProductDiscount",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Status",
                table: "ProductDiscount",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<string>(
                name: "DiscountCode",
                table: "ProductDiscount",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
