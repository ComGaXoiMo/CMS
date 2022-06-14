using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CMS_1.Migrations
{
    public partial class Dbinit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Charset",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Charset", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DoB = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeOfBusiness = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsBlock = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiftCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Decription = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiftCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProgramSize",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramSize", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RepeatSchedule",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RepeatSchedule", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Campaign",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    AutoUpdate = table.Column<bool>(type: "bit", nullable: false),
                    JoinOnlyOne = table.Column<bool>(type: "bit", nullable: false),
                    Decription = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CountCode = table.Column<int>(type: "int", nullable: false),
                    StartDay = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    EndDay = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    IdProgramSize = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Campaign", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Campaign_ProgramSize_IdProgramSize",
                        column: x => x.IdProgramSize,
                        principalTable: "ProgramSize",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RuleOfGift",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    GiftCount = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AllDay = table.Column<bool>(type: "bit", nullable: false),
                    Probability = table.Column<int>(type: "int", nullable: false),
                    ScheduleData = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    IdGiftCategory = table.Column<int>(type: "int", nullable: true),
                    IdIdRepeatSchedule = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RuleOfGift", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RuleOfGift_GiftCategory_IdGiftCategory",
                        column: x => x.IdGiftCategory,
                        principalTable: "GiftCategory",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RuleOfGift_RepeatSchedule_IdIdRepeatSchedule",
                        column: x => x.IdIdRepeatSchedule,
                        principalTable: "RepeatSchedule",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Barcode",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    BarCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    QRcode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CodeUsageLimit = table.Column<int>(type: "int", nullable: false),
                    Unlimited = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ScannedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Owner = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    IsScanned = table.Column<bool>(type: "bit", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Used = table.Column<int>(type: "int", nullable: false),
                    IdCharset = table.Column<int>(type: "int", nullable: true),
                    IdCampaign = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Barcode", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Barcode_Campaign_IdCampaign",
                        column: x => x.IdCampaign,
                        principalTable: "Campaign",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Barcode_Charset_IdCharset",
                        column: x => x.IdCharset,
                        principalTable: "Charset",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Gift",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GiftCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsageLimit = table.Column<int>(type: "int", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Used = table.Column<int>(type: "int", nullable: false),
                    IdGiftCategory = table.Column<int>(type: "int", nullable: true),
                    IdCampaign = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gift", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Gift_Campaign_IdCampaign",
                        column: x => x.IdCampaign,
                        principalTable: "Campaign",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Gift_GiftCategory_IdGiftCategory",
                        column: x => x.IdGiftCategory,
                        principalTable: "GiftCategory",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Winner",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WinDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SendGiftStatus = table.Column<bool>(type: "bit", nullable: false),
                    IdCustomer = table.Column<int>(type: "int", nullable: true),
                    IdGift = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Winner", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Winner_Customer_IdCustomer",
                        column: x => x.IdCustomer,
                        principalTable: "Customer",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Winner_Gift_IdGift",
                        column: x => x.IdGift,
                        principalTable: "Gift",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Charset",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Numbers" },
                    { 2, "Character" },
                    { 3, "All" }
                });

            migrationBuilder.InsertData(
                table: "ProgramSize",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Bulk codes" },
                    { 2, "Standalone code" }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Email", "Password" },
                values: new object[,]
                {
                    { 1, "123@gmail.com", "123Aaa" },
                    { 2, "abc@gmail.com", "123Qwe" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Barcode_Code",
                table: "Barcode",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Barcode_IdCampaign",
                table: "Barcode",
                column: "IdCampaign");

            migrationBuilder.CreateIndex(
                name: "IX_Barcode_IdCharset",
                table: "Barcode",
                column: "IdCharset");

            migrationBuilder.CreateIndex(
                name: "IX_Campaign_IdProgramSize",
                table: "Campaign",
                column: "IdProgramSize");

            migrationBuilder.CreateIndex(
                name: "IX_Campaign_Name",
                table: "Campaign",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Charset_Name",
                table: "Charset",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Gift_IdCampaign",
                table: "Gift",
                column: "IdCampaign");

            migrationBuilder.CreateIndex(
                name: "IX_Gift_IdGiftCategory",
                table: "Gift",
                column: "IdGiftCategory");

            migrationBuilder.CreateIndex(
                name: "IX_GiftCategory_Name",
                table: "GiftCategory",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProgramSize_Name",
                table: "ProgramSize",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RuleOfGift_IdGiftCategory",
                table: "RuleOfGift",
                column: "IdGiftCategory");

            migrationBuilder.CreateIndex(
                name: "IX_RuleOfGift_IdIdRepeatSchedule",
                table: "RuleOfGift",
                column: "IdIdRepeatSchedule");

            migrationBuilder.CreateIndex(
                name: "IX_User_Email",
                table: "User",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Winner_IdCustomer",
                table: "Winner",
                column: "IdCustomer");

            migrationBuilder.CreateIndex(
                name: "IX_Winner_IdGift",
                table: "Winner",
                column: "IdGift");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Barcode");

            migrationBuilder.DropTable(
                name: "RuleOfGift");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Winner");

            migrationBuilder.DropTable(
                name: "Charset");

            migrationBuilder.DropTable(
                name: "RepeatSchedule");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "Gift");

            migrationBuilder.DropTable(
                name: "Campaign");

            migrationBuilder.DropTable(
                name: "GiftCategory");

            migrationBuilder.DropTable(
                name: "ProgramSize");
        }
    }
}
