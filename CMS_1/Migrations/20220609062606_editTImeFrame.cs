using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CMS_1.Migrations
{
    public partial class editTImeFrame : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TimeFrame");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDay",
                table: "Campaign",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "EndTime",
                table: "Campaign",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDay",
                table: "Campaign",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "StartTime",
                table: "Campaign",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.CreateIndex(
                name: "IX_Campaign_Name",
                table: "Campaign",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Campaign_Name",
                table: "Campaign");

            migrationBuilder.DropColumn(
                name: "EndDay",
                table: "Campaign");

            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "Campaign");

            migrationBuilder.DropColumn(
                name: "StartDay",
                table: "Campaign");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "Campaign");

            migrationBuilder.CreateTable(
                name: "TimeFrame",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCampaign = table.Column<int>(type: "int", nullable: true),
                    EndDay = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    StartDay = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeFrame", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TimeFrame_Campaign_IdCampaign",
                        column: x => x.IdCampaign,
                        principalTable: "Campaign",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TimeFrame_IdCampaign",
                table: "TimeFrame",
                column: "IdCampaign");
        }
    }
}
