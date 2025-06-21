using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Airbnb.ReviewManagementInfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateReviewCriterials : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Accuracy",
                table: "DomainReview",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Arrival",
                table: "DomainReview",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Cleanliness",
                table: "DomainReview",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Communication",
                table: "DomainReview",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Location",
                table: "DomainReview",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PriceToQuality",
                table: "DomainReview",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Accuracy",
                table: "DomainReview");

            migrationBuilder.DropColumn(
                name: "Arrival",
                table: "DomainReview");

            migrationBuilder.DropColumn(
                name: "Cleanliness",
                table: "DomainReview");

            migrationBuilder.DropColumn(
                name: "Communication",
                table: "DomainReview");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "DomainReview");

            migrationBuilder.DropColumn(
                name: "PriceToQuality",
                table: "DomainReview");
        }
    }
}
