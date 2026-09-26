using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Pactra.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddProposals : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Engagement_Services_ServiceId_ProviderId",
                table: "Engagement");

            migrationBuilder.DropForeignKey(
                name: "FK_Engagement_Users_ClientId",
                table: "Engagement");

            migrationBuilder.DropForeignKey(
                name: "FK_Engagement_Users_ProviderId",
                table: "Engagement");

            migrationBuilder.DropForeignKey(
                name: "FK_Services_Users_ProviderId",
                table: "Services");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Engagement",
                table: "Engagement");

            migrationBuilder.RenameTable(
                name: "Engagement",
                newName: "Engagements");

            migrationBuilder.RenameIndex(
                name: "IX_Engagement_ServiceId_ProviderId",
                table: "Engagements",
                newName: "IX_Engagements_ServiceId_ProviderId");

            migrationBuilder.RenameIndex(
                name: "IX_Engagement_ProviderId",
                table: "Engagements",
                newName: "IX_Engagements_ProviderId");

            migrationBuilder.RenameIndex(
                name: "IX_Engagement_ClientId",
                table: "Engagements",
                newName: "IX_Engagements_ClientId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Engagements",
                table: "Engagements",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Proposals",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EngagementId = table.Column<long>(type: "bigint", nullable: false),
                    ProposedBy = table.Column<long>(type: "bigint", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    Description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    Terms = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    ExpiresAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    Status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proposals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Proposals_Engagements_EngagementId",
                        column: x => x.EngagementId,
                        principalTable: "Engagements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Proposals_Users_ProposedBy",
                        column: x => x.ProposedBy,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Proposals_EngagementId",
                table: "Proposals",
                column: "EngagementId");

            migrationBuilder.CreateIndex(
                name: "IX_Proposals_ProposedBy",
                table: "Proposals",
                column: "ProposedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_Engagements_Services_ServiceId_ProviderId",
                table: "Engagements",
                columns: new[] { "ServiceId", "ProviderId" },
                principalTable: "Services",
                principalColumns: new[] { "Id", "ProviderId" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Engagements_Users_ClientId",
                table: "Engagements",
                column: "ClientId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Engagements_Users_ProviderId",
                table: "Engagements",
                column: "ProviderId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Users_ProviderId",
                table: "Services",
                column: "ProviderId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Engagements_Services_ServiceId_ProviderId",
                table: "Engagements");

            migrationBuilder.DropForeignKey(
                name: "FK_Engagements_Users_ClientId",
                table: "Engagements");

            migrationBuilder.DropForeignKey(
                name: "FK_Engagements_Users_ProviderId",
                table: "Engagements");

            migrationBuilder.DropForeignKey(
                name: "FK_Services_Users_ProviderId",
                table: "Services");

            migrationBuilder.DropTable(
                name: "Proposals");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Engagements",
                table: "Engagements");

            migrationBuilder.RenameTable(
                name: "Engagements",
                newName: "Engagement");

            migrationBuilder.RenameIndex(
                name: "IX_Engagements_ServiceId_ProviderId",
                table: "Engagement",
                newName: "IX_Engagement_ServiceId_ProviderId");

            migrationBuilder.RenameIndex(
                name: "IX_Engagements_ProviderId",
                table: "Engagement",
                newName: "IX_Engagement_ProviderId");

            migrationBuilder.RenameIndex(
                name: "IX_Engagements_ClientId",
                table: "Engagement",
                newName: "IX_Engagement_ClientId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Engagement",
                table: "Engagement",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Engagement_Services_ServiceId_ProviderId",
                table: "Engagement",
                columns: new[] { "ServiceId", "ProviderId" },
                principalTable: "Services",
                principalColumns: new[] { "Id", "ProviderId" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Engagement_Users_ClientId",
                table: "Engagement",
                column: "ClientId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Engagement_Users_ProviderId",
                table: "Engagement",
                column: "ProviderId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Users_ProviderId",
                table: "Services",
                column: "ProviderId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
