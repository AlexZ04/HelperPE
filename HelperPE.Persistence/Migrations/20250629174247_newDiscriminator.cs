using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelperPE.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class newDiscriminator : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserType",
                table: "Users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserType",
                table: "Users",
                type: "character varying(21)",
                maxLength: 21,
                nullable: false,
                defaultValue: "");
        }
    }
}
