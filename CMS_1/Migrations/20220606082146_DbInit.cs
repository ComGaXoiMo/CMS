﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CMS_1.Migrations
{
    public partial class DbInit : Migration
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
                    Active = table.Column<bool>(type: "bit", nullable: false)
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
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    IdGiftCategory = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RuleOfGift", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RuleOfGift_GiftCategory_IdGiftCategory",
                        column: x => x.IdGiftCategory,
                        principalTable: "GiftCategory",
                        principalColumn: "Id");
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
                    CodeUsageLimit = table.Column<int>(type: "int", nullable: false),
                    Unlimited = table.Column<bool>(type: "bit", nullable: false),
                    CountCode = table.Column<int>(type: "int", nullable: false),
                    CodeLength = table.Column<int>(type: "int", nullable: false),
                    Prefix = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Postfix = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    IdCharset = table.Column<int>(type: "int", nullable: true),
                    IdProgramSize = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Campaign", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Campaign_Charset_IdCharset",
                        column: x => x.IdCharset,
                        principalTable: "Charset",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Campaign_ProgramSize_IdProgramSize",
                        column: x => x.IdProgramSize,
                        principalTable: "ProgramSize",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ValueSchedule",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    IdRepeat = table.Column<int>(type: "int", nullable: true),
                    IdRule = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ValueSchedule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ValueSchedule_RepeatSchedule_IdRepeat",
                        column: x => x.IdRepeat,
                        principalTable: "RepeatSchedule",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ValueSchedule_RuleOfGift_IdRule",
                        column: x => x.IdRule,
                        principalTable: "RuleOfGift",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Barcode",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    BarCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    QRcode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ScannedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Owner = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    IsScanned = table.Column<bool>(type: "bit", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
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
                });

            migrationBuilder.CreateTable(
                name: "Gift",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GiftCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Usage = table.Column<bool>(type: "bit", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
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
                name: "TimeFrame",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    StartDay = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDay = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdCampaign = table.Column<int>(type: "int", nullable: true)
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

            migrationBuilder.CreateIndex(
                name: "IX_Barcode_IdCampaign",
                table: "Barcode",
                column: "IdCampaign");

            migrationBuilder.CreateIndex(
                name: "IX_Campaign_IdCharset",
                table: "Campaign",
                column: "IdCharset");

            migrationBuilder.CreateIndex(
                name: "IX_Campaign_IdProgramSize",
                table: "Campaign",
                column: "IdProgramSize");

            migrationBuilder.CreateIndex(
                name: "IX_Gift_IdCampaign",
                table: "Gift",
                column: "IdCampaign");

            migrationBuilder.CreateIndex(
                name: "IX_Gift_IdGiftCategory",
                table: "Gift",
                column: "IdGiftCategory");

            migrationBuilder.CreateIndex(
                name: "IX_RuleOfGift_IdGiftCategory",
                table: "RuleOfGift",
                column: "IdGiftCategory");

            migrationBuilder.CreateIndex(
                name: "IX_TimeFrame_IdCampaign",
                table: "TimeFrame",
                column: "IdCampaign");

            migrationBuilder.CreateIndex(
                name: "IX_ValueSchedule_IdRepeat",
                table: "ValueSchedule",
                column: "IdRepeat");

            migrationBuilder.CreateIndex(
                name: "IX_ValueSchedule_IdRule",
                table: "ValueSchedule",
                column: "IdRule");

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
                name: "TimeFrame");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "ValueSchedule");

            migrationBuilder.DropTable(
                name: "Winner");

            migrationBuilder.DropTable(
                name: "RepeatSchedule");

            migrationBuilder.DropTable(
                name: "RuleOfGift");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "Gift");

            migrationBuilder.DropTable(
                name: "Campaign");

            migrationBuilder.DropTable(
                name: "GiftCategory");

            migrationBuilder.DropTable(
                name: "Charset");

            migrationBuilder.DropTable(
                name: "ProgramSize");
        }
    }
}
