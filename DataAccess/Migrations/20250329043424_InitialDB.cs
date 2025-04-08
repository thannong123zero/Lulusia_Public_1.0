using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitialDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TBSystem_Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBSystem_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TBSytem_Actions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Label = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBSytem_Actions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TBSytem_Modules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Label = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBSytem_Modules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TBSytem_Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsSystemRole = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBSytem_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TBSytem_Notifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsRead = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBSytem_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TBSytem_Notifications_TBSystem_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "TBSystem_Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TBSytem_UserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBSytem_UserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TBSytem_UserClaims_TBSystem_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "TBSystem_Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TBSytem_UserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBSytem_UserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_TBSytem_UserLogins_TBSystem_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "TBSystem_Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TBSytem_UserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ExpirationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBSytem_UserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_TBSytem_UserTokens_TBSystem_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "TBSystem_Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TBSytem_Controllers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModuleId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Label = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBSytem_Controllers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TBSytem_Controllers_TBSytem_Modules_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "TBSytem_Modules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TBSystem_RoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleClaimGroupId = table.Column<int>(type: "int", nullable: false),
                    RoleClaimId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBSystem_RoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TBSystem_RoleClaims_TBSytem_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "TBSytem_Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TBSytem_UserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBSytem_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_TBSytem_UserRoles_TBSystem_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "TBSystem_Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TBSytem_UserRoles_TBSytem_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "TBSytem_Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TBSytem_ControllerActions",
                columns: table => new
                {
                    ControllerId = table.Column<int>(type: "int", nullable: false),
                    ActionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBSytem_ControllerActions", x => new { x.ControllerId, x.ActionId });
                    table.ForeignKey(
                        name: "FK_TBSytem_ControllerActions_TBSytem_Actions_ActionId",
                        column: x => x.ActionId,
                        principalTable: "TBSytem_Actions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TBSytem_ControllerActions_TBSytem_Controllers_ControllerId",
                        column: x => x.ControllerId,
                        principalTable: "TBSytem_Controllers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "TBSytem_Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "CreatedBy", "CreatedOn", "Description", "IsActive", "IsDeleted", "IsSystemRole", "ModifiedBy", "ModifiedOn", "Name", "NormalizedName" },
                values: new object[] { 1, null, "System", new DateTime(2025, 3, 29, 11, 34, 23, 565, DateTimeKind.Local).AddTicks(2782), "System Admin Role", true, false, true, "System", new DateTime(2025, 3, 29, 11, 34, 23, 566, DateTimeKind.Local).AddTicks(777), "Admin", "ADMIN" });

            migrationBuilder.CreateIndex(
                name: "IX_TBSystem_RoleClaims_RoleId",
                table: "TBSystem_RoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "TBSystem_Users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_TBSystem_Users_Email",
                table: "TBSystem_Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "TBSystem_Users",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TBSytem_ControllerActions_ActionId",
                table: "TBSytem_ControllerActions",
                column: "ActionId");

            migrationBuilder.CreateIndex(
                name: "IX_TBSytem_Controllers_ModuleId",
                table: "TBSytem_Controllers",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_TBSytem_Controllers_Name",
                table: "TBSytem_Controllers",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TBSytem_Notifications_UserId",
                table: "TBSytem_Notifications",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "TBSytem_Roles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TBSytem_UserClaims_UserId",
                table: "TBSytem_UserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TBSytem_UserLogins_UserId",
                table: "TBSytem_UserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TBSytem_UserRoles_RoleId",
                table: "TBSytem_UserRoles",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TBSystem_RoleClaims");

            migrationBuilder.DropTable(
                name: "TBSytem_ControllerActions");

            migrationBuilder.DropTable(
                name: "TBSytem_Notifications");

            migrationBuilder.DropTable(
                name: "TBSytem_UserClaims");

            migrationBuilder.DropTable(
                name: "TBSytem_UserLogins");

            migrationBuilder.DropTable(
                name: "TBSytem_UserRoles");

            migrationBuilder.DropTable(
                name: "TBSytem_UserTokens");

            migrationBuilder.DropTable(
                name: "TBSytem_Actions");

            migrationBuilder.DropTable(
                name: "TBSytem_Controllers");

            migrationBuilder.DropTable(
                name: "TBSytem_Roles");

            migrationBuilder.DropTable(
                name: "TBSystem_Users");

            migrationBuilder.DropTable(
                name: "TBSytem_Modules");
        }
    }
}
