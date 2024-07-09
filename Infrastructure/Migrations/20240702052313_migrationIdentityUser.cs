using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EMSWebApp.Migrations
{
    /// <inheritdoc />
    public partial class migrationIdentityUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "EmployeeInformationtbl",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeInformationtbl_UserId",
                table: "EmployeeInformationtbl",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeInformationtbl_AspNetUsers_UserId",
                table: "EmployeeInformationtbl",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeInformationtbl_AspNetUsers_UserId",
                table: "EmployeeInformationtbl");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeInformationtbl_UserId",
                table: "EmployeeInformationtbl");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "EmployeeInformationtbl");
        }
    }
}
