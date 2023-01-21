using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shoper.Data.Migrations
{
    /// <inheritdoc />
    public partial class minoruserToCustomer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Customer",
                type: "nvarchar(450)",
                nullable: true,
                defaultValue: "",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customer_UserId",
                table: "Customer",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_AspNetUsers_UserId",
                table: "Customer",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customer_AspNetUsers_UserId",
                table: "Customer");

            migrationBuilder.DropIndex(
                name: "IX_Customer_UserId",
                table: "Customer");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Customer",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
