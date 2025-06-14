using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Airbnb.UserManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UserLanguage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserLanguages_Languages_LanguageId1",
                table: "UserLanguages");

            migrationBuilder.DropIndex(
                name: "IX_UserLanguages_LanguageId1",
                table: "UserLanguages");

            migrationBuilder.DropColumn(
                name: "LanguageId1",
                table: "UserLanguages");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LanguageId1",
                table: "UserLanguages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UserLanguages_LanguageId1",
                table: "UserLanguages",
                column: "LanguageId1");

            migrationBuilder.AddForeignKey(
                name: "FK_UserLanguages_Languages_LanguageId1",
                table: "UserLanguages",
                column: "LanguageId1",
                principalTable: "Languages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
