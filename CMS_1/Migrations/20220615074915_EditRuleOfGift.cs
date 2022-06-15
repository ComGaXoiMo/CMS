using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CMS_1.Migrations
{
    public partial class EditRuleOfGift : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RuleOfGift_Priority",
                table: "RuleOfGift");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_RuleOfGift_Priority",
                table: "RuleOfGift",
                column: "Priority",
                unique: true);
        }
    }
}
