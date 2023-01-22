using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shoper.Data.Migrations
{
    /// <inheritdoc />
    public partial class addIsActiveInOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isActive",
                table: "Order",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isActive",
                table: "Order");
        }
    }
}
