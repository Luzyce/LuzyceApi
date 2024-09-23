using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LuzyceApi.Db.AppDb.Migrations
{
    /// <inheritdoc />
    public partial class addRemarksToPP : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                table: "ProductionPlans",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "DocumentPositions",
                keyColumn: "Id",
                keyValue: 1,
                column: "StartTime",
                value: new DateTime(2024, 9, 23, 18, 24, 21, 218, DateTimeKind.Local).AddTicks(2400));

            migrationBuilder.UpdateData(
                table: "DocumentPositions",
                keyColumn: "Id",
                keyValue: 2,
                column: "StartTime",
                value: new DateTime(2024, 9, 23, 18, 24, 21, 218, DateTimeKind.Local).AddTicks(2405));

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 9, 23, 18, 24, 21, 218, DateTimeKind.Local).AddTicks(2026), new DateTime(2024, 9, 23, 18, 24, 21, 218, DateTimeKind.Local).AddTicks(2054) });

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 9, 23, 18, 24, 21, 218, DateTimeKind.Local).AddTicks(2059), new DateTime(2024, 9, 23, 18, 24, 21, 218, DateTimeKind.Local).AddTicks(2060) });

            migrationBuilder.UpdateData(
                table: "OrdersForProduction",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2024, 9, 23, 18, 24, 21, 218, DateTimeKind.Local).AddTicks(2254));

            migrationBuilder.UpdateData(
                table: "ProductionPlans",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Date", "Remarks" },
                values: new object[] { new DateOnly(2024, 9, 23), null });

            migrationBuilder.UpdateData(
                table: "Shifts",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateOnly(2024, 9, 23));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Password" },
                values: new object[] { new DateTime(2024, 9, 23, 18, 24, 21, 108, DateTimeKind.Local).AddTicks(7656), "$2a$11$7/FMs7y8r8xhyERp/Axi/.6Xk.6a.Tsrchvpv.tysMXv8r8utFvIu" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 9, 23, 18, 24, 21, 218, DateTimeKind.Local).AddTicks(1365));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2024, 9, 23, 18, 24, 21, 218, DateTimeKind.Local).AddTicks(1422));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Remarks",
                table: "ProductionPlans");

            migrationBuilder.UpdateData(
                table: "DocumentPositions",
                keyColumn: "Id",
                keyValue: 1,
                column: "StartTime",
                value: new DateTime(2024, 9, 22, 13, 15, 12, 120, DateTimeKind.Local).AddTicks(3921));

            migrationBuilder.UpdateData(
                table: "DocumentPositions",
                keyColumn: "Id",
                keyValue: 2,
                column: "StartTime",
                value: new DateTime(2024, 9, 22, 13, 15, 12, 120, DateTimeKind.Local).AddTicks(3926));

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 9, 22, 13, 15, 12, 120, DateTimeKind.Local).AddTicks(3517), new DateTime(2024, 9, 22, 13, 15, 12, 120, DateTimeKind.Local).AddTicks(3559) });

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 9, 22, 13, 15, 12, 120, DateTimeKind.Local).AddTicks(3564), new DateTime(2024, 9, 22, 13, 15, 12, 120, DateTimeKind.Local).AddTicks(3566) });

            migrationBuilder.UpdateData(
                table: "OrdersForProduction",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2024, 9, 22, 13, 15, 12, 120, DateTimeKind.Local).AddTicks(3759));

            migrationBuilder.UpdateData(
                table: "ProductionPlans",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateOnly(2024, 9, 22));

            migrationBuilder.UpdateData(
                table: "Shifts",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateOnly(2024, 9, 22));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Password" },
                values: new object[] { new DateTime(2024, 9, 22, 13, 15, 12, 11, DateTimeKind.Local).AddTicks(7744), "$2a$11$kga8cb95dNTGYGrikfcb5OFQ0dNL1SuL/VIyKsw1Eq/xyarU7zbCG" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 9, 22, 13, 15, 12, 120, DateTimeKind.Local).AddTicks(2862));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2024, 9, 22, 13, 15, 12, 120, DateTimeKind.Local).AddTicks(2915));
        }
    }
}
