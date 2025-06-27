using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelperPE.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddOtherActivitiesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OtherActivitiesEntity_Users_StudentId",
                table: "OtherActivitiesEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_OtherActivitiesEntity_Users_TeacherId",
                table: "OtherActivitiesEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OtherActivitiesEntity",
                table: "OtherActivitiesEntity");

            migrationBuilder.RenameTable(
                name: "OtherActivitiesEntity",
                newName: "OtherActivities");

            migrationBuilder.RenameIndex(
                name: "IX_OtherActivitiesEntity_TeacherId",
                table: "OtherActivities",
                newName: "IX_OtherActivities_TeacherId");

            migrationBuilder.RenameIndex(
                name: "IX_OtherActivitiesEntity_StudentId",
                table: "OtherActivities",
                newName: "IX_OtherActivities_StudentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OtherActivities",
                table: "OtherActivities",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OtherActivities_Users_StudentId",
                table: "OtherActivities",
                column: "StudentId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OtherActivities_Users_TeacherId",
                table: "OtherActivities",
                column: "TeacherId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OtherActivities_Users_StudentId",
                table: "OtherActivities");

            migrationBuilder.DropForeignKey(
                name: "FK_OtherActivities_Users_TeacherId",
                table: "OtherActivities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OtherActivities",
                table: "OtherActivities");

            migrationBuilder.RenameTable(
                name: "OtherActivities",
                newName: "OtherActivitiesEntity");

            migrationBuilder.RenameIndex(
                name: "IX_OtherActivities_TeacherId",
                table: "OtherActivitiesEntity",
                newName: "IX_OtherActivitiesEntity_TeacherId");

            migrationBuilder.RenameIndex(
                name: "IX_OtherActivities_StudentId",
                table: "OtherActivitiesEntity",
                newName: "IX_OtherActivitiesEntity_StudentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OtherActivitiesEntity",
                table: "OtherActivitiesEntity",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OtherActivitiesEntity_Users_StudentId",
                table: "OtherActivitiesEntity",
                column: "StudentId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OtherActivitiesEntity_Users_TeacherId",
                table: "OtherActivitiesEntity",
                column: "TeacherId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
