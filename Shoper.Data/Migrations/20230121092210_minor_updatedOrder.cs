using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shoper.Data.Migrations
{
    /// <inheritdoc />
    public partial class minorupdatedOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Adres1",
                table: "Order",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Adres2",
                table: "Order",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Order",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Order",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "fullName",
                table: "Order",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "mail",
                table: "Order",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Adres1",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "Adres2",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "fullName",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "mail",
                table: "Order");
        }
    }
}
