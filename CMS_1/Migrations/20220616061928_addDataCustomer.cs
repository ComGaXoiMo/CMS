using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CMS_1.Migrations
{
    public partial class addDataCustomer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "Customer");

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Id", "Address", "DoB", "IsBlock", "Name", "PhoneNumber", "Position", "TypeOfBusiness" },
                values: new object[,]
                {
                    { 1, "Quận 6, TPHCM", new DateTime(1973, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Nguyễn Hữu Huân", "0901456781", "Chủ", "Khách sạn" },
                    { 2, "Quận 5, TPHCM", new DateTime(1974, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Nguyễn Trọng Hữu", "0907852781", "Quản lý", "Nhà hàng" },
                    { 3, "Quận 7, TPHCM", new DateTime(1975, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Trần Hùng Phát", "0901485381", "Bếp", "Quán ăn" },
                    { 4, "Bến Lức, Long An", new DateTime(1976, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Lê Ngọc Anh", "0901451981", "Chủ", "Bán sỉ" },
                    { 5, "Biên Hòa, Đồng Nai", new DateTime(1977, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Lê Phan", "0901742681", "Quản lý", "Quán ăn" },
                    { 6, "Bến Lức, Long An", new DateTime(1978, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Nguyễn Thị Ngọc Hương", "0904803457", "Chủ", "Quán ăn" },
                    { 7, "Cai Lậy, Tiền Giang", new DateTime(1979, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Trần Văn Tình", "0947514514", "Chủ", "Resort" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Customer",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Customer",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Customer",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Customer",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Customer",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Customer",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Customer",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Customer",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
