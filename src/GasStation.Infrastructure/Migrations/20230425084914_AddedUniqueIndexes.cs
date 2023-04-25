using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GasStation.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedUniqueIndexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Users_Login",
                table: "Users",
                column: "Login",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reports_Title",
                table: "Reports",
                column: "Title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_Title",
                table: "Invoices",
                column: "Title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Fuels_Title",
                table: "Fuels",
                column: "Title",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_Login",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Reports_Title",
                table: "Reports");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_Title",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Fuels_Title",
                table: "Fuels");
        }
    }
}
