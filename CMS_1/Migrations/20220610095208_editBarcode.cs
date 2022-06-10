using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CMS_1.Migrations
{
    public partial class editBarcode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ScannedDate",
                table: "Barcode",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "QRcode",
                table: "Barcode",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "BarCode",
                table: "Barcode",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            migrationBuilder.InsertData(
                table: "Charset",
                columns: new[] { "Id", "Name" },
                values: new object[] { 3, "All" });

            migrationBuilder.CreateIndex(
                name: "IX_Barcode_Code",
                table: "Barcode",
                column: "Code",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Barcode_Code",
                table: "Barcode");

            migrationBuilder.DeleteData(
                table: "Charset",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ScannedDate",
                table: "Barcode",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "QRcode",
                table: "Barcode",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "BarCode",
                table: "Barcode",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);
        }
    }
}
