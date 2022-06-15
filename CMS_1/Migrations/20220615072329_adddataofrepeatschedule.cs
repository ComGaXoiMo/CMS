using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CMS_1.Migrations
{
    public partial class adddataofrepeatschedule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "RepeatSchedule",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Monthly on day" });

            migrationBuilder.InsertData(
                table: "RepeatSchedule",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "Weekly on" });

            migrationBuilder.InsertData(
                table: "RepeatSchedule",
                columns: new[] { "Id", "Name" },
                values: new object[] { 3, "Repeat daily" });

            migrationBuilder.CreateIndex(
                name: "IX_RuleOfGift_Name",
                table: "RuleOfGift",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RuleOfGift_Priority",
                table: "RuleOfGift",
                column: "Priority",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RepeatSchedule_Name",
                table: "RepeatSchedule",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RuleOfGift_Name",
                table: "RuleOfGift");

            migrationBuilder.DropIndex(
                name: "IX_RuleOfGift_Priority",
                table: "RuleOfGift");

            migrationBuilder.DropIndex(
                name: "IX_RepeatSchedule_Name",
                table: "RepeatSchedule");

            migrationBuilder.DeleteData(
                table: "RepeatSchedule",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "RepeatSchedule",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "RepeatSchedule",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
