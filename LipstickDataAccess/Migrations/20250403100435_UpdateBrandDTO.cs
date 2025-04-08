using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LipstickDataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBrandDTO : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NameEN",
                table: "Table_Brands");

            migrationBuilder.RenameColumn(
                name: "NameVN",
                table: "Table_Brands",
                newName: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Table_Brands",
                newName: "NameVN");

            migrationBuilder.AddColumn<string>(
                name: "NameEN",
                table: "Table_Brands",
                type: "nvarchar(225)",
                maxLength: 225,
                nullable: false,
                defaultValue: "");
        }
    }
}
