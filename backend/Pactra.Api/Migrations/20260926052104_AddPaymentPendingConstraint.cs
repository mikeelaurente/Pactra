using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pactra.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddPaymentPendingConstraint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Payments_EngagementId",
                table: "Payments");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_EngagementId_Type",
                table: "Payments",
                columns: new[] { "EngagementId", "Type" },
                unique: true,
                filter: "\"Status\" IN ('REQUIRED', 'PENDING')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Payments_EngagementId_Type",
                table: "Payments");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_EngagementId",
                table: "Payments",
                column: "EngagementId");
        }
    }
}
