using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CMS_1.Migrations
{
    public partial class editgift : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GiftName",
                table: "Gift",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Gift",
                keyColumn: "Id",
                keyValue: 1,
                column: "GiftName",
                value: "Hạt nêm Knorr Chay Nấm Hương 400g");

            migrationBuilder.UpdateData(
                table: "Gift",
                keyColumn: "Id",
                keyValue: 2,
                column: "GiftName",
                value: "Hạt nêm Knorr Chay Nấm Hương 400g");

            migrationBuilder.UpdateData(
                table: "Gift",
                keyColumn: "Id",
                keyValue: 3,
                column: "GiftName",
                value: "Hạt nêm Knorr Từ Thịt Thăn, Xương Ống & Tủy 600gr");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GiftName",
                table: "Gift");
        }
    }
}
