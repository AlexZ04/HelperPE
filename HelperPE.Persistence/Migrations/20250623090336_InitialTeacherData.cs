using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelperPE.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialTeacherData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "SubjectEntityTeacherEntity",
                columns: new[] { "SubjectsId", "TeachersId" },
                values: new object[] { new Guid("6a541e68-cd4c-45bc-94fb-97634ef8a3ef"), new Guid("1ea30ff4-00c9-44f9-afb9-651471a366f6") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SubjectEntityTeacherEntity",
                keyColumns: new[] { "SubjectsId", "TeachersId" },
                keyValues: new object[] { new Guid("6a541e68-cd4c-45bc-94fb-97634ef8a3ef"), new Guid("1ea30ff4-00c9-44f9-afb9-651471a366f6") });
        }
    }
}
