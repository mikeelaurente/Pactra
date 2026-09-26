using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pactra.Api.Migrations;

/// <inheritdoc />
public partial class AddProposalAcceptedConstraint : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropIndex(
            name: "IX_Proposals_EngagementId",
            table: "Proposals");

        migrationBuilder.CreateIndex(
            name: "IX_Proposals_EngagementId",
            table: "Proposals",
            column: "EngagementId",
            unique: true,
            filter: "\"Status\" = 'ACCEPTED'");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropIndex(
            name: "IX_Proposals_EngagementId",
            table: "Proposals");

        migrationBuilder.CreateIndex(
            name: "IX_Proposals_EngagementId",
            table: "Proposals",
            column: "EngagementId");
    }
}
