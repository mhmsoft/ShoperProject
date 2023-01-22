using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shoper.Data.Migrations
{
    /// <inheritdoc />
    public partial class addConfirmInOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isVerified",
                table: "Order",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isVerified",
                table: "Order");
        }
    }
}
