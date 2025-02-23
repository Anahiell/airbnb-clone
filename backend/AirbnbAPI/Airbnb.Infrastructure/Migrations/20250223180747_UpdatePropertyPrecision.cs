using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Airbnb.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePropertyPrecision : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "PropertyEntity",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "PropertyEntity",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "OwnerId",
                table: "PropertyEntity",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "PropertyEntity",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "PropertyEntity",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "PropertyEntity",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "Location", "OwnerId", "Price", "Title" },
                values: new object[] { "Nice place", "New York", 0, 100m, "Cozy Apartment" });

            migrationBuilder.UpdateData(
                table: "PropertyEntity",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "Location", "OwnerId", "Price", "Title" },
                values: new object[] { "Big villa with pool", "Los Angeles", 0, 300m, "Luxury Villa" });

            migrationBuilder.UpdateData(
                table: "PropertyEntity",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "Location", "OwnerId", "Price", "Title" },
                values: new object[] { "Cheap and cozy", "San Francisco", 0, 50m, "Small Studio" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "PropertyEntity");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "PropertyEntity");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "PropertyEntity");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "PropertyEntity");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "PropertyEntity");
        }
    }
}
