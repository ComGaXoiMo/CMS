using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CMS_1.Migrations
{
    public partial class add_spin_for_barcode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "SpinDate",
                table: "Barcode",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "UseForSpin",
                table: "Barcode",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SpinDate",
                table: "Barcode");

            migrationBuilder.DropColumn(
                name: "UseForSpin",
                table: "Barcode");
        }
    }
}
