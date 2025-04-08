using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LipstickDataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NameEN",
                table: "Table_PageTypes");

            migrationBuilder.DropColumn(
                name: "NameVN",
                table: "Table_PageTypes");

            migrationBuilder.AlterColumn<string>(
                name: "Label",
                table: "Table_PageTypes",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Table_PageTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "Table_PageTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Table_PageTypes",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Table_PageTypes");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "Table_PageTypes");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Table_PageTypes");

            migrationBuilder.AlterColumn<string>(
                name: "Label",
                table: "Table_PageTypes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<string>(
                name: "NameEN",
                table: "Table_PageTypes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NameVN",
                table: "Table_PageTypes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
