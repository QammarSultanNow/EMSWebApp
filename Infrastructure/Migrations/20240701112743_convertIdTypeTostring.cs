using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EMSWebApp.Migrations
{
    /// <inheritdoc />
    public partial class convertIdTypeTostring : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ModifiedBy",
                table: "EmployeeInformationtbl",
                newName: "ModifiedBy");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "EmployeeInformationtbl",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ModifiedBy",
                table: "EmployeeInformationtbl",
                newName: "ModifiedOnBy");

            migrationBuilder.AlterColumn<int>(
                name: "CreatedBy",
                table: "EmployeeInformationtbl",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
