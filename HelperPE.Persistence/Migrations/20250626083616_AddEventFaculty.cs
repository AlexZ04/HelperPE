using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelperPE.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddEventFaculty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "FacultyId",
                table: "Events",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Events_FacultyId",
                table: "Events",
                column: "FacultyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Faculties_FacultyId",
                table: "Events",
                column: "FacultyId",
                principalTable: "Faculties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Faculties_FacultyId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_FacultyId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "FacultyId",
                table: "Events");
        }
    }
}
