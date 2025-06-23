using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HelperPE.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Faculties",
                keyColumn: "Id",
                keyValue: new Guid("4af95a15-1a44-4a08-8557-811749f035a4"));

            migrationBuilder.DeleteData(
                table: "Faculties",
                keyColumn: "Id",
                keyValue: new Guid("7c838d98-2222-4fb0-9c89-f25e7e95092c"));

            migrationBuilder.DeleteData(
                table: "Faculties",
                keyColumn: "Id",
                keyValue: new Guid("812da553-d5e2-4d25-ab27-4576779683a4"));

            migrationBuilder.InsertData(
                table: "Faculties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("12345678-1234-1234-1234-123456789012"), "Отдел подготовки кадров высшей квалификации" },
                    { new Guid("23456789-2345-2345-2345-234567890123"), "Факультет иностранных языков" },
                    { new Guid("34567890-3456-3456-3456-345678901234"), "САЕ Институт «Умные материалы и технологии»" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Faculties",
                keyColumn: "Id",
                keyValue: new Guid("12345678-1234-1234-1234-123456789012"));

            migrationBuilder.DeleteData(
                table: "Faculties",
                keyColumn: "Id",
                keyValue: new Guid("23456789-2345-2345-2345-234567890123"));

            migrationBuilder.DeleteData(
                table: "Faculties",
                keyColumn: "Id",
                keyValue: new Guid("34567890-3456-3456-3456-345678901234"));

            migrationBuilder.InsertData(
                table: "Faculties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("4af95a15-1a44-4a08-8557-811749f035a4"), "Факультет иностранных языков" },
                    { new Guid("7c838d98-2222-4fb0-9c89-f25e7e95092c"), "Отдел подготовки кадров высшей квалификации" },
                    { new Guid("812da553-d5e2-4d25-ab27-4576779683a4"), "САЕ Институт «Умные материалы и технологии»" }
                });
        }
    }
}
