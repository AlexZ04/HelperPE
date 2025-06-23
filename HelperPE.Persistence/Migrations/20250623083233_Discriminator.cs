using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelperPE.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Discriminator : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Users");

            migrationBuilder.AddColumn<DateTime>(
                name: "AppointmentDate",
                table: "Users",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserType",
                table: "Users",
                type: "character varying(21)",
                maxLength: 21,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "CuratorEntityId",
                table: "Faculties",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SportsOrganizerEntityId",
                table: "Events",
                type: "uuid",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Faculties",
                keyColumn: "Id",
                keyValue: new Guid("12345678-1234-1234-1234-123456789012"),
                column: "CuratorEntityId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Faculties",
                keyColumn: "Id",
                keyValue: new Guid("23456789-2345-2345-2345-234567890123"),
                column: "CuratorEntityId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Faculties",
                keyColumn: "Id",
                keyValue: new Guid("34567890-3456-3456-3456-345678901234"),
                column: "CuratorEntityId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Faculties_CuratorEntityId",
                table: "Faculties",
                column: "CuratorEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_SportsOrganizerEntityId",
                table: "Events",
                column: "SportsOrganizerEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Users_SportsOrganizerEntityId",
                table: "Events",
                column: "SportsOrganizerEntityId",
                principalTable: "Users",
                principalColumn: "Id");

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
                name: "FK_Events_Users_SportsOrganizerEntityId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Faculties_Users_CuratorEntityId",
                table: "Faculties");

            migrationBuilder.DropIndex(
                name: "IX_Faculties_CuratorEntityId",
                table: "Faculties");

            migrationBuilder.DropIndex(
                name: "IX_Events_SportsOrganizerEntityId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "AppointmentDate",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserType",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CuratorEntityId",
                table: "Faculties");

            migrationBuilder.DropColumn(
                name: "SportsOrganizerEntityId",
                table: "Events");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Users",
                type: "character varying(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");
        }
    }
}
