using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LuzyceApi.Db.AppDb.Migrations
{
    /// <inheritdoc />
    public partial class changeWeightToDecimal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "WeightNetto",
                table: "LampshadeNorms",
                type: "decimal(8,2)",
                precision: 8,
                scale: 2,
                nullable: true,
                oldClrType: typeof(double),
                oldType: "double",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "WeightBrutto",
                table: "LampshadeNorms",
                type: "decimal(8,2)",
                precision: 8,
                scale: 2,
                nullable: true,
                oldClrType: typeof(double),
                oldType: "double",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "DocumentPositions",
                keyColumn: "Id",
                keyValue: 1,
                column: "StartTime",
                value: new DateTime(2024, 10, 13, 18, 8, 7, 56, DateTimeKind.Local).AddTicks(6994));

            migrationBuilder.UpdateData(
                table: "DocumentPositions",
                keyColumn: "Id",
                keyValue: 2,
                column: "StartTime",
                value: new DateTime(2024, 10, 13, 18, 8, 7, 56, DateTimeKind.Local).AddTicks(7001));

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 10, 13, 18, 8, 7, 56, DateTimeKind.Local).AddTicks(6545), new DateTime(2024, 10, 13, 18, 8, 7, 56, DateTimeKind.Local).AddTicks(6565) });

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 10, 13, 18, 8, 7, 56, DateTimeKind.Local).AddTicks(6571), new DateTime(2024, 10, 13, 18, 8, 7, 56, DateTimeKind.Local).AddTicks(6575) });

            migrationBuilder.UpdateData(
                table: "LampshadeNorms",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "WeightBrutto", "WeightNetto" },
                values: new object[] { 3m, 0.45m });

            migrationBuilder.UpdateData(
                table: "OrdersForProduction",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2024, 10, 13, 18, 8, 7, 56, DateTimeKind.Local).AddTicks(6787));

            migrationBuilder.UpdateData(
                table: "ProductionPlans",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateOnly(2024, 10, 13));

            migrationBuilder.UpdateData(
                table: "Shifts",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateOnly(2024, 10, 13));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Password" },
                values: new object[] { new DateTime(2024, 10, 13, 18, 8, 6, 949, DateTimeKind.Local).AddTicks(6637), "$2a$11$so/gl0HcMpp3sibl6Zf43uRi2wXehbnLIhwjZVHKRsxmBWodb1e7u" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 10, 13, 18, 8, 7, 56, DateTimeKind.Local).AddTicks(5907));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2024, 10, 13, 18, 8, 7, 56, DateTimeKind.Local).AddTicks(5972));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "WeightNetto",
                table: "LampshadeNorms",
                type: "double",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)",
                oldPrecision: 8,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "WeightBrutto",
                table: "LampshadeNorms",
                type: "double",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,2)",
                oldPrecision: 8,
                oldScale: 2,
                oldNullable: true);

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
                table: "LampshadeNorms",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "WeightBrutto", "WeightNetto" },
                values: new object[] { 3.0, 0.45000000000000001 });

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
                column: "Date",
                value: new DateOnly(2024, 9, 23));

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
    }
}
