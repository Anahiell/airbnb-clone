using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Airbnb.PictureManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PictureRoomId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RoomId",
                table: "ProductPictures",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "ProductPictures");
        }
    }
}
