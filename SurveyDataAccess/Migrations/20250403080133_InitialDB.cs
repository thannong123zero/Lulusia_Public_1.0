using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SurveyDataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitialDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Table_QuestionGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameEN = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    NameVN = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    Priority = table.Column<byte>(type: "tinyint", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedBy = table.Column<string>(type: "varchar(100)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "varchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Table_QuestionGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Table_QuestionTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Table_QuestionTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Table_SurveyForms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MallId = table.Column<int>(type: "int", nullable: true),
                    OfficeId = table.Column<int>(type: "int", nullable: true),
                    IsPeriodic = table.Column<bool>(type: "bit", nullable: false),
                    NameEN = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    NameVN = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    TitleEN = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    TitleVN = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    DescriptionEN = table.Column<string>(type: "nvarchar(1000)", nullable: false),
                    DescriptionVN = table.Column<string>(type: "nvarchar(1000)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "varchar(100)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "varchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Table_SurveyForms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Table_Questions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionGroupId = table.Column<int>(type: "int", nullable: false),
                    QuestionTypeId = table.Column<int>(type: "int", nullable: false),
                    ChartLabel = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    NameEN = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    NameVN = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedBy = table.Column<string>(type: "varchar(100)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "varchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Table_Questions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Table_Questions_Table_QuestionGroups_QuestionGroupId",
                        column: x => x.QuestionGroupId,
                        principalTable: "Table_QuestionGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Table_Questions_Table_QuestionTypes_QuestionTypeId",
                        column: x => x.QuestionTypeId,
                        principalTable: "Table_QuestionTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Table_Participants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SurveyFormId = table.Column<int>(type: "int", nullable: false),
                    PhoneNumber = table.Column<string>(type: "varchar(10)", nullable: true),
                    Email = table.Column<string>(type: "varchar(255)", nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedBy = table.Column<string>(type: "varchar(100)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "varchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Table_Participants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Table_Participants_Table_SurveyForms_SurveyFormId",
                        column: x => x.SurveyFormId,
                        principalTable: "Table_SurveyForms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Table_PredefinedAnswers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    Point = table.Column<byte>(type: "tinyint", nullable: false),
                    NameVN = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    NameEN = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Table_PredefinedAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Table_PredefinedAnswers_Table_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Table_Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Table_SurveyQuestions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SurveyFormId = table.Column<int>(type: "int", nullable: false),
                    QuestionGroupId = table.Column<int>(type: "int", nullable: false),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    Priority = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Table_SurveyQuestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Table_SurveyQuestions_Table_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Table_Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Table_SurveyQuestions_Table_SurveyForms_SurveyFormId",
                        column: x => x.SurveyFormId,
                        principalTable: "Table_SurveyForms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Table_Answers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParticipantId = table.Column<int>(type: "int", nullable: false),
                    QuestionGroupId = table.Column<int>(type: "int", nullable: false),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    QuestionTypeId = table.Column<int>(type: "int", nullable: false),
                    AnswerId = table.Column<int>(type: "int", nullable: true),
                    Answer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rating = table.Column<byte>(type: "tinyint", nullable: true),
                    Point = table.Column<byte>(type: "tinyint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Table_Answers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Table_Answers_Table_Participants_ParticipantId",
                        column: x => x.ParticipantId,
                        principalTable: "Table_Participants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Table_QuestionTypes",
                columns: new[] { "Id", "CreatedOn", "Description", "IsActive", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 4, 3, 15, 1, 32, 962, DateTimeKind.Local).AddTicks(8880), "Câu hỏi đóng (Closed-ended question) – Chỉ có các câu trả lời sẵn.", true, "Câu hỏi đóng" },
                    { 2, new DateTime(2025, 4, 3, 15, 1, 32, 963, DateTimeKind.Local).AddTicks(8267), "Câu hỏi mở (Open-ended question) – Người dùng nhập câu trả lời.", true, "Câu hỏi mở" },
                    { 3, new DateTime(2025, 4, 3, 15, 1, 32, 963, DateTimeKind.Local).AddTicks(8276), "Câu hỏi kết hợp (Hybrid question) hoặc Câu hỏi mở rộng (Extended question) – Vừa có câu trả lời sẵn, vừa cho phép người dùng nhập câu trả lời riêng.", true, "Câu hỏi kết hợp" },
                    { 4, new DateTime(2025, 4, 3, 15, 1, 32, 963, DateTimeKind.Local).AddTicks(8277), "Cẩu hỏi đáng giá (rating question) - Cho người dùng đánh giá mức độ trên 5 sao.", true, "Câu hỏi đánh giá" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Table_Answers_ParticipantId",
                table: "Table_Answers",
                column: "ParticipantId");

            migrationBuilder.CreateIndex(
                name: "IX_Table_Participants_SurveyFormId",
                table: "Table_Participants",
                column: "SurveyFormId");

            migrationBuilder.CreateIndex(
                name: "IX_Table_PredefinedAnswers_QuestionId",
                table: "Table_PredefinedAnswers",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Table_Questions_QuestionGroupId",
                table: "Table_Questions",
                column: "QuestionGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Table_Questions_QuestionTypeId",
                table: "Table_Questions",
                column: "QuestionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Table_SurveyQuestions_QuestionId",
                table: "Table_SurveyQuestions",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Table_SurveyQuestions_SurveyFormId",
                table: "Table_SurveyQuestions",
                column: "SurveyFormId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Table_Answers");

            migrationBuilder.DropTable(
                name: "Table_PredefinedAnswers");

            migrationBuilder.DropTable(
                name: "Table_SurveyQuestions");

            migrationBuilder.DropTable(
                name: "Table_Participants");

            migrationBuilder.DropTable(
                name: "Table_Questions");

            migrationBuilder.DropTable(
                name: "Table_SurveyForms");

            migrationBuilder.DropTable(
                name: "Table_QuestionGroups");

            migrationBuilder.DropTable(
                name: "Table_QuestionTypes");
        }
    }
}
