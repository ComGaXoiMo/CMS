using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CMS_1.Migrations
{
    public partial class editGift : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Usage",
                table: "Gift");

            migrationBuilder.AddColumn<int>(
                name: "UsageLimit",
                table: "Gift",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_GiftCategory_Name",
                table: "GiftCategory",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_GiftCategory_Name",
                table: "GiftCategory");

            migrationBuilder.DropColumn(
                name: "UsageLimit",
                table: "Gift");

            migrationBuilder.AddColumn<bool>(
                name: "Usage",
                table: "Gift",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
