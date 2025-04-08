using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddPriority : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Priority",
                table: "TBSytem_Actions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "TBSytem_Roles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "ModifiedOn" },
                values: new object[] { new DateTime(2025, 3, 31, 9, 37, 26, 154, DateTimeKind.Local).AddTicks(2406), new DateTime(2025, 3, 31, 9, 37, 26, 155, DateTimeKind.Local).AddTicks(2467) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Priority",
                table: "TBSytem_Actions");

            migrationBuilder.UpdateData(
                table: "TBSytem_Roles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "ModifiedOn" },
                values: new object[] { new DateTime(2025, 3, 29, 11, 34, 23, 565, DateTimeKind.Local).AddTicks(2782), new DateTime(2025, 3, 29, 11, 34, 23, 566, DateTimeKind.Local).AddTicks(777) });
        }
    }
}
