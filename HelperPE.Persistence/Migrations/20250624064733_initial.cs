using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HelperPE.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    EventId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    ClassesAmount = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    SportsOrganizerEntityId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.EventId);
                });

            migrationBuilder.CreateTable(
                name: "EventsAttendances",
                columns: table => new
                {
                    EventId = table.Column<Guid>(type: "uuid", nullable: false),
                    StudentId = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventsAttendances", x => new { x.StudentId, x.EventId });
                    table.ForeignKey(
                        name: "FK_EventsAttendances_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "EventId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Faculties",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CuratorEntityId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Faculties", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    FullName = table.Column<string>(type: "text", nullable: false),
                    Role = table.Column<int>(type: "integer", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    UserType = table.Column<string>(type: "character varying(21)", maxLength: 21, nullable: false),
                    Course = table.Column<int>(type: "integer", nullable: true),
                    Group = table.Column<string>(type: "text", nullable: true),
                    FacultyId = table.Column<Guid>(type: "uuid", nullable: true),
                    AppointmentDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Faculties_FacultyId",
                        column: x => x.FacultyId,
                        principalTable: "Faculties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OtherActivitiesEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Comment = table.Column<string>(type: "text", nullable: false),
                    ClassesAmount = table.Column<int>(type: "integer", nullable: false),
                    StudentId = table.Column<Guid>(type: "uuid", nullable: false),
                    TeacherId = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OtherActivitiesEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OtherActivitiesEntity_Users_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OtherActivitiesEntity_Users_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pairs",
                columns: table => new
                {
                    PairId = table.Column<Guid>(type: "uuid", nullable: false),
                    PairNumber = table.Column<int>(type: "integer", nullable: false),
                    TeacherId = table.Column<Guid>(type: "uuid", nullable: false),
                    SubjectId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pairs", x => x.PairId);
                    table.ForeignKey(
                        name: "FK_Pairs_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pairs_Users_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Token = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Expires = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubjectEntityTeacherEntity",
                columns: table => new
                {
                    SubjectsId = table.Column<Guid>(type: "uuid", nullable: false),
                    TeachersId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectEntityTeacherEntity", x => new { x.SubjectsId, x.TeachersId });
                    table.ForeignKey(
                        name: "FK_SubjectEntityTeacherEntity_Subjects_SubjectsId",
                        column: x => x.SubjectsId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubjectEntityTeacherEntity_Users_TeachersId",
                        column: x => x.TeachersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PairsAttendances",
                columns: table => new
                {
                    StudentId = table.Column<Guid>(type: "uuid", nullable: false),
                    PairId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClassesAmount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PairsAttendances", x => new { x.StudentId, x.PairId });
                    table.ForeignKey(
                        name: "FK_PairsAttendances_Pairs_PairId",
                        column: x => x.PairId,
                        principalTable: "Pairs",
                        principalColumn: "PairId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PairsAttendances_Users_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Faculties",
                columns: new[] { "Id", "CuratorEntityId", "Name" },
                values: new object[,]
                {
                    { new Guid("12345678-1234-1234-1234-123456789012"), null, "Отдел подготовки кадров высшей квалификации" },
                    { new Guid("23456789-2345-2345-2345-234567890123"), null, "Факультет иностранных языков" },
                    { new Guid("34567890-3456-3456-3456-345678901234"), null, "САЕ Институт «Умные материалы и технологии»" },
                    { new Guid("3f339655-3c00-4c8d-991e-7708eb5bee6c"), null, "НОЦ «Высшая ИТ-Школа»" }
                });

            migrationBuilder.InsertData(
                table: "Subjects",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("6a541e68-cd4c-45bc-94fb-97634ef8a3ef"), "Баскетбол" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FullName", "Password", "Role", "UserType" },
                values: new object[] { new Guid("1ea30ff4-00c9-44f9-afb9-651471a366f6"), "peteacher@example.com", "Thomas Zane", "$2a$11$Ug2z7Jxu7srXwiGMEuqfK.MW7uXoH.hP/VsjtygCSobdtJwldDl/q", 2, "Teacher" });

            migrationBuilder.InsertData(
                table: "SubjectEntityTeacherEntity",
                columns: new[] { "SubjectsId", "TeachersId" },
                values: new object[] { new Guid("6a541e68-cd4c-45bc-94fb-97634ef8a3ef"), new Guid("1ea30ff4-00c9-44f9-afb9-651471a366f6") });

            migrationBuilder.CreateIndex(
                name: "IX_Events_SportsOrganizerEntityId",
                table: "Events",
                column: "SportsOrganizerEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_EventsAttendances_EventId",
                table: "EventsAttendances",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Faculties_CuratorEntityId",
                table: "Faculties",
                column: "CuratorEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_OtherActivitiesEntity_StudentId",
                table: "OtherActivitiesEntity",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_OtherActivitiesEntity_TeacherId",
                table: "OtherActivitiesEntity",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_Pairs_SubjectId",
                table: "Pairs",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Pairs_TeacherId",
                table: "Pairs",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_PairsAttendances_PairId",
                table: "PairsAttendances",
                column: "PairId");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserId",
                table: "RefreshTokens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectEntityTeacherEntity_TeachersId",
                table: "SubjectEntityTeacherEntity",
                column: "TeachersId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_FacultyId",
                table: "Users",
                column: "FacultyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Users_SportsOrganizerEntityId",
                table: "Events",
                column: "SportsOrganizerEntityId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EventsAttendances_Users_StudentId",
                table: "EventsAttendances",
                column: "StudentId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Faculties_Users_CuratorEntityId",
                table: "Faculties",
                column: "CuratorEntityId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Faculties_Users_CuratorEntityId",
                table: "Faculties");

            migrationBuilder.DropTable(
                name: "EventsAttendances");

            migrationBuilder.DropTable(
                name: "OtherActivitiesEntity");

            migrationBuilder.DropTable(
                name: "PairsAttendances");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "SubjectEntityTeacherEntity");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Pairs");

            migrationBuilder.DropTable(
                name: "Subjects");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Faculties");
        }
    }
}
