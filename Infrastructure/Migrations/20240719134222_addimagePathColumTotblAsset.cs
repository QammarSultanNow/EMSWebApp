using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EMSWebApp.Migrations
{
    /// <inheritdoc />
    public partial class addimagePathColumTotblAsset : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "tblAssets",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "tblAssets");
        }
    }
}
