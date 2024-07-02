using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EMSWebApp.Migrations
{
    /// <inheritdoc />
    public partial class addforiegnkey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeInformationtbl_DepartmentId",
                table: "EmployeeInformationtbl",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeInformationtbl_tblDepartment_DepartmentId",
                table: "EmployeeInformationtbl",
                column: "DepartmentId",
                principalTable: "tblDepartment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeInformationtbl_tblDepartment_DepartmentId",
                table: "EmployeeInformationtbl");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeInformationtbl_DepartmentId",
                table: "EmployeeInformationtbl");

           
        }
    }
}
