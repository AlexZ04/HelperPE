using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelperPE.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddAttendanceStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "PairsAttendances",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "PairsAttendances");
        }
    }
}
