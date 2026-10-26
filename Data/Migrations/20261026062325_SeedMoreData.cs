using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PopPop.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedMoreData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "fbcad7b2-f545-4c63-8337-f7adacba6d58");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "481a46f1-206a-4e75-bed6-893a0569336f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "ce2b0c57-c43d-45a5-8fa4-9fd44e0e1b9b");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-001",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5336cfb7-e6b0-46a0-944a-8b862c73bc55", "AQAAAAIAAYagAAAAEOgDAa2IObWSVYVqlqcszoIHA5N2mQj3W6QRnKPnI7Hn28z5UkPjLktIhZRW4q3tBA==", "e43821f8-8664-47f1-a77b-a2bda06ff0a6" });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Thương hiệu Thương hiệu 01 cho thể thao cầu lông", "Thương hiệu 01" });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "Description", "Name", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Thương hiệu Thương hiệu 02 cho thể thao cầu lông", "Thương hiệu 02", new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "Description", "Name", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 1, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Thương hiệu Thương hiệu 03 cho thể thao cầu lông", "Thương hiệu 03", new DateTime(2024, 1, 3, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "CreatedAt", "Description", "IsActive", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { 4, new DateTime(2024, 1, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Thương hiệu Thương hiệu 04 cho thể thao cầu lông", true, "Thương hiệu 04", new DateTime(2024, 1, 4, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 5, new DateTime(2024, 1, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Thương hiệu Thương hiệu 05 cho thể thao cầu lông", true, "Thương hiệu 05", new DateTime(2024, 1, 5, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 6, new DateTime(2024, 1, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Thương hiệu Thương hiệu 06 cho thể thao cầu lông", true, "Thương hiệu 06", new DateTime(2024, 1, 6, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 7, new DateTime(2024, 1, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Thương hiệu Thương hiệu 07 cho thể thao cầu lông", true, "Thương hiệu 07", new DateTime(2024, 1, 7, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 8, new DateTime(2024, 1, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Thương hiệu Thương hiệu 08 cho thể thao cầu lông", true, "Thương hiệu 08", new DateTime(2024, 1, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 9, new DateTime(2024, 1, 9, 0, 0, 0, 0, DateTimeKind.Utc), "Thương hiệu Thương hiệu 09 cho thể thao cầu lông", true, "Thương hiệu 09", new DateTime(2024, 1, 9, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 10, new DateTime(2024, 1, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Thương hiệu Thương hiệu 10 cho thể thao cầu lông", true, "Thương hiệu 10", new DateTime(2024, 1, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 11, new DateTime(2024, 1, 11, 0, 0, 0, 0, DateTimeKind.Utc), "Thương hiệu Thương hiệu 11 cho thể thao cầu lông", true, "Thương hiệu 11", new DateTime(2024, 1, 11, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 12, new DateTime(2024, 1, 12, 0, 0, 0, 0, DateTimeKind.Utc), "Thương hiệu Thương hiệu 12 cho thể thao cầu lông", true, "Thương hiệu 12", new DateTime(2024, 1, 12, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 13, new DateTime(2024, 1, 13, 0, 0, 0, 0, DateTimeKind.Utc), "Thương hiệu Thương hiệu 13 cho thể thao cầu lông", true, "Thương hiệu 13", new DateTime(2024, 1, 13, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 14, new DateTime(2024, 1, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Thương hiệu Thương hiệu 14 cho thể thao cầu lông", true, "Thương hiệu 14", new DateTime(2024, 1, 14, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 15, new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Thương hiệu Thương hiệu 15 cho thể thao cầu lông", true, "Thương hiệu 15", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 16, new DateTime(2024, 1, 16, 0, 0, 0, 0, DateTimeKind.Utc), "Thương hiệu Thương hiệu 16 cho thể thao cầu lông", true, "Thương hiệu 16", new DateTime(2024, 1, 16, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 17, new DateTime(2024, 1, 17, 0, 0, 0, 0, DateTimeKind.Utc), "Thương hiệu Thương hiệu 17 cho thể thao cầu lông", true, "Thương hiệu 17", new DateTime(2024, 1, 17, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 18, new DateTime(2024, 1, 18, 0, 0, 0, 0, DateTimeKind.Utc), "Thương hiệu Thương hiệu 18 cho thể thao cầu lông", true, "Thương hiệu 18", new DateTime(2024, 1, 18, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 19, new DateTime(2024, 1, 19, 0, 0, 0, 0, DateTimeKind.Utc), "Thương hiệu Thương hiệu 19 cho thể thao cầu lông", true, "Thương hiệu 19", new DateTime(2024, 1, 19, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 20, new DateTime(2024, 1, 20, 0, 0, 0, 0, DateTimeKind.Utc), "Thương hiệu Thương hiệu 20 cho thể thao cầu lông", true, "Thương hiệu 20", new DateTime(2024, 1, 20, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 21, new DateTime(2024, 1, 21, 0, 0, 0, 0, DateTimeKind.Utc), "Thương hiệu Thương hiệu 21 cho thể thao cầu lông", true, "Thương hiệu 21", new DateTime(2024, 1, 21, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 22, new DateTime(2024, 1, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Thương hiệu Thương hiệu 22 cho thể thao cầu lông", true, "Thương hiệu 22", new DateTime(2024, 1, 22, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 23, new DateTime(2024, 1, 23, 0, 0, 0, 0, DateTimeKind.Utc), "Thương hiệu Thương hiệu 23 cho thể thao cầu lông", true, "Thương hiệu 23", new DateTime(2024, 1, 23, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 24, new DateTime(2024, 1, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Thương hiệu Thương hiệu 24 cho thể thao cầu lông", true, "Thương hiệu 24", new DateTime(2024, 1, 24, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 25, new DateTime(2024, 1, 25, 0, 0, 0, 0, DateTimeKind.Utc), "Thương hiệu Thương hiệu 25 cho thể thao cầu lông", true, "Thương hiệu 25", new DateTime(2024, 1, 25, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 26, new DateTime(2024, 1, 26, 0, 0, 0, 0, DateTimeKind.Utc), "Thương hiệu Thương hiệu 26 cho thể thao cầu lông", true, "Thương hiệu 26", new DateTime(2024, 1, 26, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 27, new DateTime(2024, 1, 27, 0, 0, 0, 0, DateTimeKind.Utc), "Thương hiệu Thương hiệu 27 cho thể thao cầu lông", true, "Thương hiệu 27", new DateTime(2024, 1, 27, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 28, new DateTime(2024, 1, 28, 0, 0, 0, 0, DateTimeKind.Utc), "Thương hiệu Thương hiệu 28 cho thể thao cầu lông", true, "Thương hiệu 28", new DateTime(2024, 1, 28, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 29, new DateTime(2024, 1, 29, 0, 0, 0, 0, DateTimeKind.Utc), "Thương hiệu Thương hiệu 29 cho thể thao cầu lông", true, "Thương hiệu 29", new DateTime(2024, 1, 29, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 30, new DateTime(2024, 1, 30, 0, 0, 0, 0, DateTimeKind.Utc), "Thương hiệu Thương hiệu 30 cho thể thao cầu lông", true, "Thương hiệu 30", new DateTime(2024, 1, 30, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 31, new DateTime(2024, 1, 31, 0, 0, 0, 0, DateTimeKind.Utc), "Thương hiệu Thương hiệu 31 cho thể thao cầu lông", true, "Thương hiệu 31", new DateTime(2024, 1, 31, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 32, new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Thương hiệu Thương hiệu 32 cho thể thao cầu lông", true, "Thương hiệu 32", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 33, new DateTime(2024, 2, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Thương hiệu Thương hiệu 33 cho thể thao cầu lông", true, "Thương hiệu 33", new DateTime(2024, 2, 2, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 34, new DateTime(2024, 2, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Thương hiệu Thương hiệu 34 cho thể thao cầu lông", true, "Thương hiệu 34", new DateTime(2024, 2, 3, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 35, new DateTime(2024, 2, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Thương hiệu Thương hiệu 35 cho thể thao cầu lông", true, "Thương hiệu 35", new DateTime(2024, 2, 4, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 36, new DateTime(2024, 2, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Thương hiệu Thương hiệu 36 cho thể thao cầu lông", true, "Thương hiệu 36", new DateTime(2024, 2, 5, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 37, new DateTime(2024, 2, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Thương hiệu Thương hiệu 37 cho thể thao cầu lông", true, "Thương hiệu 37", new DateTime(2024, 2, 6, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 38, new DateTime(2024, 2, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Thương hiệu Thương hiệu 38 cho thể thao cầu lông", true, "Thương hiệu 38", new DateTime(2024, 2, 7, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 39, new DateTime(2024, 2, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Thương hiệu Thương hiệu 39 cho thể thao cầu lông", true, "Thương hiệu 39", new DateTime(2024, 2, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 40, new DateTime(2024, 2, 9, 0, 0, 0, 0, DateTimeKind.Utc), "Thương hiệu Thương hiệu 40 cho thể thao cầu lông", true, "Thương hiệu 40", new DateTime(2024, 2, 9, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Danh mục Danh mục cầu lông 01", "Danh mục cầu lông 01" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "Description", "Name", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Danh mục Danh mục cầu lông 02", "Danh mục cầu lông 02", new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "Description", "Name", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 1, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Danh mục Danh mục cầu lông 03", "Danh mục cầu lông 03", new DateTime(2024, 1, 3, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "Description", "Name", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 1, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Danh mục Danh mục cầu lông 04", "Danh mục cầu lông 04", new DateTime(2024, 1, 4, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "Description", "Name", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 1, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Danh mục Danh mục cầu lông 05", "Danh mục cầu lông 05", new DateTime(2024, 1, 5, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "Description", "IsActive", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { 6, new DateTime(2024, 1, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Danh mục Danh mục cầu lông 06", true, "Danh mục cầu lông 06", new DateTime(2024, 1, 6, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 7, new DateTime(2024, 1, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Danh mục Danh mục cầu lông 07", true, "Danh mục cầu lông 07", new DateTime(2024, 1, 7, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 8, new DateTime(2024, 1, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Danh mục Danh mục cầu lông 08", true, "Danh mục cầu lông 08", new DateTime(2024, 1, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 9, new DateTime(2024, 1, 9, 0, 0, 0, 0, DateTimeKind.Utc), "Danh mục Danh mục cầu lông 09", true, "Danh mục cầu lông 09", new DateTime(2024, 1, 9, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 10, new DateTime(2024, 1, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Danh mục Danh mục cầu lông 10", true, "Danh mục cầu lông 10", new DateTime(2024, 1, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 11, new DateTime(2024, 1, 11, 0, 0, 0, 0, DateTimeKind.Utc), "Danh mục Danh mục cầu lông 11", true, "Danh mục cầu lông 11", new DateTime(2024, 1, 11, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 12, new DateTime(2024, 1, 12, 0, 0, 0, 0, DateTimeKind.Utc), "Danh mục Danh mục cầu lông 12", true, "Danh mục cầu lông 12", new DateTime(2024, 1, 12, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 13, new DateTime(2024, 1, 13, 0, 0, 0, 0, DateTimeKind.Utc), "Danh mục Danh mục cầu lông 13", true, "Danh mục cầu lông 13", new DateTime(2024, 1, 13, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 14, new DateTime(2024, 1, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Danh mục Danh mục cầu lông 14", true, "Danh mục cầu lông 14", new DateTime(2024, 1, 14, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 15, new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Danh mục Danh mục cầu lông 15", true, "Danh mục cầu lông 15", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 16, new DateTime(2024, 1, 16, 0, 0, 0, 0, DateTimeKind.Utc), "Danh mục Danh mục cầu lông 16", true, "Danh mục cầu lông 16", new DateTime(2024, 1, 16, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 17, new DateTime(2024, 1, 17, 0, 0, 0, 0, DateTimeKind.Utc), "Danh mục Danh mục cầu lông 17", true, "Danh mục cầu lông 17", new DateTime(2024, 1, 17, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 18, new DateTime(2024, 1, 18, 0, 0, 0, 0, DateTimeKind.Utc), "Danh mục Danh mục cầu lông 18", true, "Danh mục cầu lông 18", new DateTime(2024, 1, 18, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 19, new DateTime(2024, 1, 19, 0, 0, 0, 0, DateTimeKind.Utc), "Danh mục Danh mục cầu lông 19", true, "Danh mục cầu lông 19", new DateTime(2024, 1, 19, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 20, new DateTime(2024, 1, 20, 0, 0, 0, 0, DateTimeKind.Utc), "Danh mục Danh mục cầu lông 20", true, "Danh mục cầu lông 20", new DateTime(2024, 1, 20, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 21, new DateTime(2024, 1, 21, 0, 0, 0, 0, DateTimeKind.Utc), "Danh mục Danh mục cầu lông 21", true, "Danh mục cầu lông 21", new DateTime(2024, 1, 21, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 22, new DateTime(2024, 1, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Danh mục Danh mục cầu lông 22", true, "Danh mục cầu lông 22", new DateTime(2024, 1, 22, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 23, new DateTime(2024, 1, 23, 0, 0, 0, 0, DateTimeKind.Utc), "Danh mục Danh mục cầu lông 23", true, "Danh mục cầu lông 23", new DateTime(2024, 1, 23, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 24, new DateTime(2024, 1, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Danh mục Danh mục cầu lông 24", true, "Danh mục cầu lông 24", new DateTime(2024, 1, 24, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 25, new DateTime(2024, 1, 25, 0, 0, 0, 0, DateTimeKind.Utc), "Danh mục Danh mục cầu lông 25", true, "Danh mục cầu lông 25", new DateTime(2024, 1, 25, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 26, new DateTime(2024, 1, 26, 0, 0, 0, 0, DateTimeKind.Utc), "Danh mục Danh mục cầu lông 26", true, "Danh mục cầu lông 26", new DateTime(2024, 1, 26, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 27, new DateTime(2024, 1, 27, 0, 0, 0, 0, DateTimeKind.Utc), "Danh mục Danh mục cầu lông 27", true, "Danh mục cầu lông 27", new DateTime(2024, 1, 27, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 28, new DateTime(2024, 1, 28, 0, 0, 0, 0, DateTimeKind.Utc), "Danh mục Danh mục cầu lông 28", true, "Danh mục cầu lông 28", new DateTime(2024, 1, 28, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 29, new DateTime(2024, 1, 29, 0, 0, 0, 0, DateTimeKind.Utc), "Danh mục Danh mục cầu lông 29", true, "Danh mục cầu lông 29", new DateTime(2024, 1, 29, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 30, new DateTime(2024, 1, 30, 0, 0, 0, 0, DateTimeKind.Utc), "Danh mục Danh mục cầu lông 30", true, "Danh mục cầu lông 30", new DateTime(2024, 1, 30, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 31, new DateTime(2024, 1, 31, 0, 0, 0, 0, DateTimeKind.Utc), "Danh mục Danh mục cầu lông 31", true, "Danh mục cầu lông 31", new DateTime(2024, 1, 31, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 32, new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Danh mục Danh mục cầu lông 32", true, "Danh mục cầu lông 32", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 33, new DateTime(2024, 2, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Danh mục Danh mục cầu lông 33", true, "Danh mục cầu lông 33", new DateTime(2024, 2, 2, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 34, new DateTime(2024, 2, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Danh mục Danh mục cầu lông 34", true, "Danh mục cầu lông 34", new DateTime(2024, 2, 3, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 35, new DateTime(2024, 2, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Danh mục Danh mục cầu lông 35", true, "Danh mục cầu lông 35", new DateTime(2024, 2, 4, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 36, new DateTime(2024, 2, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Danh mục Danh mục cầu lông 36", true, "Danh mục cầu lông 36", new DateTime(2024, 2, 5, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 37, new DateTime(2024, 2, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Danh mục Danh mục cầu lông 37", true, "Danh mục cầu lông 37", new DateTime(2024, 2, 6, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 38, new DateTime(2024, 2, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Danh mục Danh mục cầu lông 38", true, "Danh mục cầu lông 38", new DateTime(2024, 2, 7, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 39, new DateTime(2024, 2, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Danh mục Danh mục cầu lông 39", true, "Danh mục cầu lông 39", new DateTime(2024, 2, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 40, new DateTime(2024, 2, 9, 0, 0, 0, 0, DateTimeKind.Utc), "Danh mục Danh mục cầu lông 40", true, "Danh mục cầu lông 40", new DateTime(2024, 2, 9, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "ProductImages",
                columns: new[] { "Id", "ImageUrl", "IsPrimary", "ProductId", "SortOrder" },
                values: new object[,]
                {
                    { 1, "/img/product/p1.jpg", true, 1, 1 },
                    { 2, "/img/product/p2.jpg", false, 1, 2 },
                    { 3, "/img/product/p2.jpg", true, 2, 1 },
                    { 4, "/img/product/p3.jpg", false, 2, 2 },
                    { 5, "/img/product/p3.jpg", true, 3, 1 },
                    { 6, "/img/product/p4.jpg", false, 3, 2 }
                });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Price", "SKU", "StockQuantity", "VariantLabel" },
                values: new object[] { 535000m, "POP001-1", 17, "Màu Đen" });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Price", "ProductId", "SKU", "StockQuantity", "VariantLabel" },
                values: new object[] { 685000m, 1, "POP001-2", 18, "Màu Trắng" });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Price", "ProductId", "SKU", "StockQuantity", "VariantLabel" },
                values: new object[] { 670000m, 2, "POP002-1", 18, "Màu Đen" });

            migrationBuilder.InsertData(
                table: "ProductVariants",
                columns: new[] { "Id", "IsActive", "Price", "ProductId", "SKU", "StockQuantity", "VariantLabel" },
                values: new object[,]
                {
                    { 4, true, 820000m, 2, "POP002-2", 19, "Màu Trắng" },
                    { 5, true, 805000m, 3, "POP003-1", 19, "Màu Đen" },
                    { 6, true, 955000m, 3, "POP003-2", 20, "Màu Trắng" }
                });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Description", "Name", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Sản phẩm 1 dành cho luyện tập và thi đấu cầu lông, chất lượng cao và bền bỉ.", "Vợt Yonex Astrox 99 1", new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "Description", "Name", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 1, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Sản phẩm 2 dành cho luyện tập và thi đấu cầu lông, chất lượng cao và bền bỉ.", "Vợt Yonex Nanoflare 800 2", new DateTime(2024, 1, 3, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "Description", "Name", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 1, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Sản phẩm 3 dành cho luyện tập và thi đấu cầu lông, chất lượng cao và bền bỉ.", "Vợt Victor Brave Sword 12 3", new DateTime(2024, 1, 4, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "BrandId", "CategoryId", "CreatedAt", "Description", "IsActive", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { 13, 1, 1, new DateTime(2024, 1, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Sản phẩm 13 dành cho luyện tập và thi đấu cầu lông, chất lượng cao và bền bỉ.", true, "Túi đựng vợt Yonex 13", new DateTime(2024, 1, 14, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 14, 2, 2, new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Sản phẩm 14 dành cho luyện tập và thi đấu cầu lông, chất lượng cao và bền bỉ.", true, "Túi đựng vợt Victor 14", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 15, 3, 3, new DateTime(2024, 1, 16, 0, 0, 0, 0, DateTimeKind.Utc), "Sản phẩm 15 dành cho luyện tập và thi đấu cầu lông, chất lượng cao và bền bỉ.", true, "Lưới cầu lông Li-Ning 15", new DateTime(2024, 1, 16, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 25, 1, 1, new DateTime(2024, 1, 26, 0, 0, 0, 0, DateTimeKind.Utc), "Sản phẩm 25 dành cho luyện tập và thi đấu cầu lông, chất lượng cao và bền bỉ.", true, "Giày Head Sprint 25", new DateTime(2024, 1, 26, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 26, 2, 2, new DateTime(2024, 1, 27, 0, 0, 0, 0, DateTimeKind.Utc), "Sản phẩm 26 dành cho luyện tập và thi đấu cầu lông, chất lượng cao và bền bỉ.", true, "Giày Adidas Court 26", new DateTime(2024, 1, 27, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 27, 3, 3, new DateTime(2024, 1, 28, 0, 0, 0, 0, DateTimeKind.Utc), "Sản phẩm 27 dành cho luyện tập và thi đấu cầu lông, chất lượng cao và bền bỉ.", true, "Giày Nike Zoom 27", new DateTime(2024, 1, 28, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 37, 1, 1, new DateTime(2024, 2, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Sản phẩm 37 dành cho luyện tập và thi đấu cầu lông, chất lượng cao và bền bỉ.", true, "Vợt cho người chuyên nghiệp 37", new DateTime(2024, 2, 7, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 38, 2, 2, new DateTime(2024, 2, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Sản phẩm 38 dành cho luyện tập và thi đấu cầu lông, chất lượng cao và bền bỉ.", true, "Giày đệm chống chấn 38", new DateTime(2024, 2, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 39, 3, 3, new DateTime(2024, 2, 9, 0, 0, 0, 0, DateTimeKind.Utc), "Sản phẩm 39 dành cho luyện tập và thi đấu cầu lông, chất lượng cao và bền bỉ.", true, "Áo khoác thời trang 39", new DateTime(2024, 2, 9, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 49, 1, 1, new DateTime(2024, 2, 19, 0, 0, 0, 0, DateTimeKind.Utc), "Sản phẩm 49 dành cho luyện tập và thi đấu cầu lông, chất lượng cao và bền bỉ.", true, "Giày nhẹ bám sân 49", new DateTime(2024, 2, 19, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 50, 2, 2, new DateTime(2024, 2, 20, 0, 0, 0, 0, DateTimeKind.Utc), "Sản phẩm 50 dành cho luyện tập và thi đấu cầu lông, chất lượng cao và bền bỉ.", true, "Áo chống nắng 50", new DateTime(2024, 2, 20, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 51, 3, 3, new DateTime(2024, 2, 21, 0, 0, 0, 0, DateTimeKind.Utc), "Sản phẩm 51 dành cho luyện tập và thi đấu cầu lông, chất lượng cao và bền bỉ.", true, "Băng quấn cổ tay chuyên dụng 51", new DateTime(2024, 2, 21, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "ProductImages",
                columns: new[] { "Id", "ImageUrl", "IsPrimary", "ProductId", "SortOrder" },
                values: new object[,]
                {
                    { 25, "/img/product/p5.jpg", true, 13, 1 },
                    { 26, "/img/product/p6.jpg", false, 13, 2 },
                    { 27, "/img/product/p6.jpg", true, 14, 1 },
                    { 28, "/img/product/p7.jpg", false, 14, 2 },
                    { 29, "/img/product/p7.jpg", true, 15, 1 },
                    { 30, "/img/product/p8.jpg", false, 15, 2 },
                    { 49, "/img/product/p1.jpg", true, 25, 1 },
                    { 50, "/img/product/p2.jpg", false, 25, 2 },
                    { 51, "/img/product/p2.jpg", true, 26, 1 },
                    { 52, "/img/product/p3.jpg", false, 26, 2 },
                    { 53, "/img/product/p3.jpg", true, 27, 1 },
                    { 54, "/img/product/p4.jpg", false, 27, 2 },
                    { 73, "/img/product/p5.jpg", true, 37, 1 },
                    { 74, "/img/product/p6.jpg", false, 37, 2 },
                    { 75, "/img/product/p6.jpg", true, 38, 1 },
                    { 76, "/img/product/p7.jpg", false, 38, 2 },
                    { 77, "/img/product/p7.jpg", true, 39, 1 },
                    { 78, "/img/product/p8.jpg", false, 39, 2 },
                    { 97, "/img/product/p1.jpg", true, 49, 1 },
                    { 98, "/img/product/p2.jpg", false, 49, 2 },
                    { 99, "/img/product/p2.jpg", true, 50, 1 },
                    { 100, "/img/product/p3.jpg", false, 50, 2 },
                    { 101, "/img/product/p3.jpg", true, 51, 1 },
                    { 102, "/img/product/p4.jpg", false, 51, 2 }
                });

            migrationBuilder.InsertData(
                table: "ProductVariants",
                columns: new[] { "Id", "IsActive", "Price", "ProductId", "SKU", "StockQuantity", "VariantLabel" },
                values: new object[,]
                {
                    { 25, true, 2155000m, 13, "POP013-1", 29, "Màu Đen" },
                    { 26, true, 2305000m, 13, "POP013-2", 30, "Màu Trắng" },
                    { 27, true, 2290000m, 14, "POP014-1", 30, "Màu Đen" },
                    { 28, true, 2440000m, 14, "POP014-2", 31, "Màu Trắng" },
                    { 29, true, 2425000m, 15, "POP015-1", 31, "Màu Đen" },
                    { 30, true, 2575000m, 15, "POP015-2", 32, "Màu Trắng" },
                    { 49, true, 3775000m, 25, "POP025-1", 41, "Màu Đen" },
                    { 50, true, 3925000m, 25, "POP025-2", 42, "Màu Trắng" },
                    { 51, true, 3910000m, 26, "POP026-1", 42, "Màu Đen" },
                    { 52, true, 4060000m, 26, "POP026-2", 43, "Màu Trắng" },
                    { 53, true, 4045000m, 27, "POP027-1", 43, "Màu Đen" },
                    { 54, true, 4195000m, 27, "POP027-2", 44, "Màu Trắng" },
                    { 73, true, 5395000m, 37, "POP037-1", 23, "Màu Đen" },
                    { 74, true, 5545000m, 37, "POP037-2", 24, "Màu Trắng" },
                    { 75, true, 530000m, 38, "POP038-1", 24, "Màu Đen" },
                    { 76, true, 680000m, 38, "POP038-2", 25, "Màu Trắng" },
                    { 77, true, 665000m, 39, "POP039-1", 25, "Màu Đen" },
                    { 78, true, 815000m, 39, "POP039-2", 26, "Màu Trắng" },
                    { 97, true, 2015000m, 49, "POP049-1", 35, "Màu Đen" },
                    { 98, true, 2165000m, 49, "POP049-2", 36, "Màu Trắng" },
                    { 99, true, 2150000m, 50, "POP050-1", 36, "Màu Đen" },
                    { 100, true, 2300000m, 50, "POP050-2", 37, "Màu Trắng" },
                    { 101, true, 2285000m, 51, "POP051-1", 37, "Màu Đen" },
                    { 102, true, 2435000m, 51, "POP051-2", 38, "Màu Trắng" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "BrandId", "CategoryId", "CreatedAt", "Description", "IsActive", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { 4, 4, 4, new DateTime(2024, 1, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Sản phẩm 4 dành cho luyện tập và thi đấu cầu lông, chất lượng cao và bền bỉ.", true, "Vợt Li-Ning Turbo 900 4", new DateTime(2024, 1, 5, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 5, 5, 5, new DateTime(2024, 1, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Sản phẩm 5 dành cho luyện tập và thi đấu cầu lông, chất lượng cao và bền bỉ.", true, "Giày Yonex Power Cushion 5", new DateTime(2024, 1, 6, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 6, 6, 6, new DateTime(2024, 1, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Sản phẩm 6 dành cho luyện tập và thi đấu cầu lông, chất lượng cao và bền bỉ.", true, "Giày Victor A830 6", new DateTime(2024, 1, 7, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 7, 7, 7, new DateTime(2024, 1, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Sản phẩm 7 dành cho luyện tập và thi đấu cầu lông, chất lượng cao và bền bỉ.", true, "Giày Li-Ning Sprint 7", new DateTime(2024, 1, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 8, 8, 8, new DateTime(2024, 1, 9, 0, 0, 0, 0, DateTimeKind.Utc), "Sản phẩm 8 dành cho luyện tập và thi đấu cầu lông, chất lượng cao và bền bỉ.", true, "Giày Mizuno Wave Rider 8", new DateTime(2024, 1, 9, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 9, 9, 9, new DateTime(2024, 1, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Sản phẩm 9 dành cho luyện tập và thi đấu cầu lông, chất lượng cao và bền bỉ.", true, "Áo Yonex Training 9", new DateTime(2024, 1, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 10, 10, 10, new DateTime(2024, 1, 11, 0, 0, 0, 0, DateTimeKind.Utc), "Sản phẩm 10 dành cho luyện tập và thi đấu cầu lông, chất lượng cao và bền bỉ.", true, "Áo Victor Sport 10", new DateTime(2024, 1, 11, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 11, 11, 11, new DateTime(2024, 1, 12, 0, 0, 0, 0, DateTimeKind.Utc), "Sản phẩm 11 dành cho luyện tập và thi đấu cầu lông, chất lượng cao và bền bỉ.", true, "Áo Li-Ning Pro 11", new DateTime(2024, 1, 12, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 12, 12, 12, new DateTime(2024, 1, 13, 0, 0, 0, 0, DateTimeKind.Utc), "Sản phẩm 12 dành cho luyện tập và thi đấu cầu lông, chất lượng cao và bền bỉ.", true, "Áo Mizuno Active 12", new DateTime(2024, 1, 13, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 16, 4, 4, new DateTime(2024, 1, 17, 0, 0, 0, 0, DateTimeKind.Utc), "Sản phẩm 16 dành cho luyện tập và thi đấu cầu lông, chất lượng cao và bền bỉ.", true, "Bóng cầu lông Yonex 16", new DateTime(2024, 1, 17, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 17, 5, 5, new DateTime(2024, 1, 18, 0, 0, 0, 0, DateTimeKind.Utc), "Sản phẩm 17 dành cho luyện tập và thi đấu cầu lông, chất lượng cao và bền bỉ.", true, "Băng quấn cổ tay 17", new DateTime(2024, 1, 18, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 18, 6, 6, new DateTime(2024, 1, 19, 0, 0, 0, 0, DateTimeKind.Utc), "Sản phẩm 18 dành cho luyện tập và thi đấu cầu lông, chất lượng cao và bền bỉ.", true, "Dây căng vợt 3U 18", new DateTime(2024, 1, 19, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 19, 7, 7, new DateTime(2024, 1, 20, 0, 0, 0, 0, DateTimeKind.Utc), "Sản phẩm 19 dành cho luyện tập và thi đấu cầu lông, chất lượng cao và bền bỉ.", true, "Dây căng vợt 4U 19", new DateTime(2024, 1, 20, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 20, 8, 8, new DateTime(2024, 1, 21, 0, 0, 0, 0, DateTimeKind.Utc), "Sản phẩm 20 dành cho luyện tập và thi đấu cầu lông, chất lượng cao và bền bỉ.", true, "Balo thể thao Pro 20", new DateTime(2024, 1, 21, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 21, 9, 9, new DateTime(2024, 1, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Sản phẩm 21 dành cho luyện tập và thi đấu cầu lông, chất lượng cao và bền bỉ.", true, "Cầu lông chuyên nghiệp 21", new DateTime(2024, 1, 22, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 22, 10, 10, new DateTime(2024, 1, 23, 0, 0, 0, 0, DateTimeKind.Utc), "Sản phẩm 22 dành cho luyện tập và thi đấu cầu lông, chất lượng cao và bền bỉ.", true, "Vợt Head Graphene 360 22", new DateTime(2024, 1, 23, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 23, 11, 11, new DateTime(2024, 1, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Sản phẩm 23 dành cho luyện tập và thi đấu cầu lông, chất lượng cao và bền bỉ.", true, "Vợt Babolat X100 23", new DateTime(2024, 1, 24, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 24, 12, 12, new DateTime(2024, 1, 25, 0, 0, 0, 0, DateTimeKind.Utc), "Sản phẩm 24 dành cho luyện tập và thi đấu cầu lông, chất lượng cao và bền bỉ.", true, "Vợt Apacs Z-Force 24", new DateTime(2024, 1, 25, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 28, 4, 4, new DateTime(2024, 1, 29, 0, 0, 0, 0, DateTimeKind.Utc), "Sản phẩm 28 dành cho luyện tập và thi đấu cầu lông, chất lượng cao và bền bỉ.", true, "Áo Nike Court 28", new DateTime(2024, 1, 29, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 29, 5, 5, new DateTime(2024, 1, 30, 0, 0, 0, 0, DateTimeKind.Utc), "Sản phẩm 29 dành cho luyện tập và thi đấu cầu lông, chất lượng cao và bền bỉ.", true, "Áo Puma Energy 29", new DateTime(2024, 1, 30, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 30, 6, 6, new DateTime(2024, 1, 31, 0, 0, 0, 0, DateTimeKind.Utc), "Sản phẩm 30 dành cho luyện tập và thi đấu cầu lông, chất lượng cao và bền bỉ.", true, "Túi vợt Head 30", new DateTime(2024, 1, 31, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 31, 7, 7, new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Sản phẩm 31 dành cho luyện tập và thi đấu cầu lông, chất lượng cao và bền bỉ.", true, "Lưới đánh bóng 31", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 32, 8, 8, new DateTime(2024, 2, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Sản phẩm 32 dành cho luyện tập và thi đấu cầu lông, chất lượng cao và bền bỉ.", true, "Bóng cầu lông premium 32", new DateTime(2024, 2, 2, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 33, 9, 9, new DateTime(2024, 2, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Sản phẩm 33 dành cho luyện tập và thi đấu cầu lông, chất lượng cao và bền bỉ.", true, "Dụng cụ bảo vệ vợt 33", new DateTime(2024, 2, 3, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 34, 10, 10, new DateTime(2024, 2, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Sản phẩm 34 dành cho luyện tập và thi đấu cầu lông, chất lượng cao và bền bỉ.", true, "Bọc cán vợt 34", new DateTime(2024, 2, 4, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 35, 11, 11, new DateTime(2024, 2, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Sản phẩm 35 dành cho luyện tập và thi đấu cầu lông, chất lượng cao và bền bỉ.", true, "Vợt đi kèm dây căng 35", new DateTime(2024, 2, 5, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 36, 12, 12, new DateTime(2024, 2, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Sản phẩm 36 dành cho luyện tập và thi đấu cầu lông, chất lượng cao và bền bỉ.", true, "Vợt cho người mới 36", new DateTime(2024, 2, 6, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 40, 4, 4, new DateTime(2024, 2, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Sản phẩm 40 dành cho luyện tập và thi đấu cầu lông, chất lượng cao và bền bỉ.", true, "Áo chạy bộ thể thao 40", new DateTime(2024, 2, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 41, 5, 5, new DateTime(2024, 2, 11, 0, 0, 0, 0, DateTimeKind.Utc), "Sản phẩm 41 dành cho luyện tập và thi đấu cầu lông, chất lượng cao và bền bỉ.", true, "Vợt thể thao đa năng 41", new DateTime(2024, 2, 11, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 42, 6, 6, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Utc), "Sản phẩm 42 dành cho luyện tập và thi đấu cầu lông, chất lượng cao và bền bỉ.", true, "Giày chạy bộ cầu lông 42", new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 43, 7, 7, new DateTime(2024, 2, 13, 0, 0, 0, 0, DateTimeKind.Utc), "Sản phẩm 43 dành cho luyện tập và thi đấu cầu lông, chất lượng cao và bền bỉ.", true, "Balo di chuyển thể thao 43", new DateTime(2024, 2, 13, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 44, 8, 8, new DateTime(2024, 2, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Sản phẩm 44 dành cho luyện tập và thi đấu cầu lông, chất lượng cao và bền bỉ.", true, "Bọc bảo vệ vợt 44", new DateTime(2024, 2, 14, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 45, 9, 9, new DateTime(2024, 2, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Sản phẩm 45 dành cho luyện tập và thi đấu cầu lông, chất lượng cao và bền bỉ.", true, "Lưới thi đấu tiêu chuẩn 45", new DateTime(2024, 2, 15, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 46, 10, 10, new DateTime(2024, 2, 16, 0, 0, 0, 0, DateTimeKind.Utc), "Sản phẩm 46 dành cho luyện tập và thi đấu cầu lông, chất lượng cao và bền bỉ.", true, "Bóng chất lượng cao 46", new DateTime(2024, 2, 16, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 47, 11, 11, new DateTime(2024, 2, 17, 0, 0, 0, 0, DateTimeKind.Utc), "Sản phẩm 47 dành cho luyện tập và thi đấu cầu lông, chất lượng cao và bền bỉ.", true, "Vợt niềm tin 47", new DateTime(2024, 2, 17, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 48, 12, 12, new DateTime(2024, 2, 18, 0, 0, 0, 0, DateTimeKind.Utc), "Sản phẩm 48 dành cho luyện tập và thi đấu cầu lông, chất lượng cao và bền bỉ.", true, "Vợt kết hợp công nghệ 48", new DateTime(2024, 2, 18, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 52, 4, 4, new DateTime(2024, 2, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Sản phẩm 52 dành cho luyện tập và thi đấu cầu lông, chất lượng cao và bền bỉ.", true, "Dây căng vợt tối ưu 52", new DateTime(2024, 2, 22, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 53, 5, 5, new DateTime(2024, 2, 23, 0, 0, 0, 0, DateTimeKind.Utc), "Sản phẩm 53 dành cho luyện tập và thi đấu cầu lông, chất lượng cao và bền bỉ.", true, "Túi chứa phụ kiện 53", new DateTime(2024, 2, 23, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 54, 6, 6, new DateTime(2024, 2, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Sản phẩm 54 dành cho luyện tập và thi đấu cầu lông, chất lượng cao và bền bỉ.", true, "Vợt săn điểm cao 54", new DateTime(2024, 2, 24, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 55, 7, 7, new DateTime(2024, 2, 25, 0, 0, 0, 0, DateTimeKind.Utc), "Sản phẩm 55 dành cho luyện tập và thi đấu cầu lông, chất lượng cao và bền bỉ.", true, "Vợt đấm mạnh 55", new DateTime(2024, 2, 25, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 56, 8, 8, new DateTime(2024, 2, 26, 0, 0, 0, 0, DateTimeKind.Utc), "Sản phẩm 56 dành cho luyện tập và thi đấu cầu lông, chất lượng cao và bền bỉ.", true, "Vợt khóa gió 56", new DateTime(2024, 2, 26, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 57, 9, 9, new DateTime(2024, 2, 27, 0, 0, 0, 0, DateTimeKind.Utc), "Sản phẩm 57 dành cho luyện tập và thi đấu cầu lông, chất lượng cao và bền bỉ.", true, "Giày cải thiện phản xạ 57", new DateTime(2024, 2, 27, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 58, 10, 10, new DateTime(2024, 2, 28, 0, 0, 0, 0, DateTimeKind.Utc), "Sản phẩm 58 dành cho luyện tập và thi đấu cầu lông, chất lượng cao và bền bỉ.", true, "Áo thể thao mùa hè 58", new DateTime(2024, 2, 28, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 59, 11, 11, new DateTime(2024, 2, 29, 0, 0, 0, 0, DateTimeKind.Utc), "Sản phẩm 59 dành cho luyện tập và thi đấu cầu lông, chất lượng cao và bền bỉ.", true, "Balo cho vận động viên 59", new DateTime(2024, 2, 29, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 60, 12, 12, new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Sản phẩm 60 dành cho luyện tập và thi đấu cầu lông, chất lượng cao và bền bỉ.", true, "Vợt Yonex Astrox 99 60", new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "ProductImages",
                columns: new[] { "Id", "ImageUrl", "IsPrimary", "ProductId", "SortOrder" },
                values: new object[,]
                {
                    { 7, "/img/product/p4.jpg", true, 4, 1 },
                    { 8, "/img/product/p5.jpg", false, 4, 2 },
                    { 9, "/img/product/p5.jpg", true, 5, 1 },
                    { 10, "/img/product/p6.jpg", false, 5, 2 },
                    { 11, "/img/product/p6.jpg", true, 6, 1 },
                    { 12, "/img/product/p7.jpg", false, 6, 2 },
                    { 13, "/img/product/p7.jpg", true, 7, 1 },
                    { 14, "/img/product/p8.jpg", false, 7, 2 },
                    { 15, "/img/product/p8.jpg", true, 8, 1 },
                    { 16, "/img/product/p1.jpg", false, 8, 2 },
                    { 17, "/img/product/p1.jpg", true, 9, 1 },
                    { 18, "/img/product/p2.jpg", false, 9, 2 },
                    { 19, "/img/product/p2.jpg", true, 10, 1 },
                    { 20, "/img/product/p3.jpg", false, 10, 2 },
                    { 21, "/img/product/p3.jpg", true, 11, 1 },
                    { 22, "/img/product/p4.jpg", false, 11, 2 },
                    { 23, "/img/product/p4.jpg", true, 12, 1 },
                    { 24, "/img/product/p5.jpg", false, 12, 2 },
                    { 31, "/img/product/p8.jpg", true, 16, 1 },
                    { 32, "/img/product/p1.jpg", false, 16, 2 },
                    { 33, "/img/product/p1.jpg", true, 17, 1 },
                    { 34, "/img/product/p2.jpg", false, 17, 2 },
                    { 35, "/img/product/p2.jpg", true, 18, 1 },
                    { 36, "/img/product/p3.jpg", false, 18, 2 },
                    { 37, "/img/product/p3.jpg", true, 19, 1 },
                    { 38, "/img/product/p4.jpg", false, 19, 2 },
                    { 39, "/img/product/p4.jpg", true, 20, 1 },
                    { 40, "/img/product/p5.jpg", false, 20, 2 },
                    { 41, "/img/product/p5.jpg", true, 21, 1 },
                    { 42, "/img/product/p6.jpg", false, 21, 2 },
                    { 43, "/img/product/p6.jpg", true, 22, 1 },
                    { 44, "/img/product/p7.jpg", false, 22, 2 },
                    { 45, "/img/product/p7.jpg", true, 23, 1 },
                    { 46, "/img/product/p8.jpg", false, 23, 2 },
                    { 47, "/img/product/p8.jpg", true, 24, 1 },
                    { 48, "/img/product/p1.jpg", false, 24, 2 },
                    { 55, "/img/product/p4.jpg", true, 28, 1 },
                    { 56, "/img/product/p5.jpg", false, 28, 2 },
                    { 57, "/img/product/p5.jpg", true, 29, 1 },
                    { 58, "/img/product/p6.jpg", false, 29, 2 },
                    { 59, "/img/product/p6.jpg", true, 30, 1 },
                    { 60, "/img/product/p7.jpg", false, 30, 2 },
                    { 61, "/img/product/p7.jpg", true, 31, 1 },
                    { 62, "/img/product/p8.jpg", false, 31, 2 },
                    { 63, "/img/product/p8.jpg", true, 32, 1 },
                    { 64, "/img/product/p1.jpg", false, 32, 2 },
                    { 65, "/img/product/p1.jpg", true, 33, 1 },
                    { 66, "/img/product/p2.jpg", false, 33, 2 },
                    { 67, "/img/product/p2.jpg", true, 34, 1 },
                    { 68, "/img/product/p3.jpg", false, 34, 2 },
                    { 69, "/img/product/p3.jpg", true, 35, 1 },
                    { 70, "/img/product/p4.jpg", false, 35, 2 },
                    { 71, "/img/product/p4.jpg", true, 36, 1 },
                    { 72, "/img/product/p5.jpg", false, 36, 2 },
                    { 79, "/img/product/p8.jpg", true, 40, 1 },
                    { 80, "/img/product/p1.jpg", false, 40, 2 },
                    { 81, "/img/product/p1.jpg", true, 41, 1 },
                    { 82, "/img/product/p2.jpg", false, 41, 2 },
                    { 83, "/img/product/p2.jpg", true, 42, 1 },
                    { 84, "/img/product/p3.jpg", false, 42, 2 },
                    { 85, "/img/product/p3.jpg", true, 43, 1 },
                    { 86, "/img/product/p4.jpg", false, 43, 2 },
                    { 87, "/img/product/p4.jpg", true, 44, 1 },
                    { 88, "/img/product/p5.jpg", false, 44, 2 },
                    { 89, "/img/product/p5.jpg", true, 45, 1 },
                    { 90, "/img/product/p6.jpg", false, 45, 2 },
                    { 91, "/img/product/p6.jpg", true, 46, 1 },
                    { 92, "/img/product/p7.jpg", false, 46, 2 },
                    { 93, "/img/product/p7.jpg", true, 47, 1 },
                    { 94, "/img/product/p8.jpg", false, 47, 2 },
                    { 95, "/img/product/p8.jpg", true, 48, 1 },
                    { 96, "/img/product/p1.jpg", false, 48, 2 },
                    { 103, "/img/product/p4.jpg", true, 52, 1 },
                    { 104, "/img/product/p5.jpg", false, 52, 2 },
                    { 105, "/img/product/p5.jpg", true, 53, 1 },
                    { 106, "/img/product/p6.jpg", false, 53, 2 },
                    { 107, "/img/product/p6.jpg", true, 54, 1 },
                    { 108, "/img/product/p7.jpg", false, 54, 2 },
                    { 109, "/img/product/p7.jpg", true, 55, 1 },
                    { 110, "/img/product/p8.jpg", false, 55, 2 },
                    { 111, "/img/product/p8.jpg", true, 56, 1 },
                    { 112, "/img/product/p1.jpg", false, 56, 2 },
                    { 113, "/img/product/p1.jpg", true, 57, 1 },
                    { 114, "/img/product/p2.jpg", false, 57, 2 },
                    { 115, "/img/product/p2.jpg", true, 58, 1 },
                    { 116, "/img/product/p3.jpg", false, 58, 2 },
                    { 117, "/img/product/p3.jpg", true, 59, 1 },
                    { 118, "/img/product/p4.jpg", false, 59, 2 },
                    { 119, "/img/product/p4.jpg", true, 60, 1 },
                    { 120, "/img/product/p5.jpg", false, 60, 2 }
                });

            migrationBuilder.InsertData(
                table: "ProductVariants",
                columns: new[] { "Id", "IsActive", "Price", "ProductId", "SKU", "StockQuantity", "VariantLabel" },
                values: new object[,]
                {
                    { 7, true, 940000m, 4, "POP004-1", 20, "Màu Đen" },
                    { 8, true, 1090000m, 4, "POP004-2", 21, "Màu Trắng" },
                    { 9, true, 1075000m, 5, "POP005-1", 21, "Màu Đen" },
                    { 10, true, 1225000m, 5, "POP005-2", 22, "Màu Trắng" },
                    { 11, true, 1210000m, 6, "POP006-1", 22, "Màu Đen" },
                    { 12, true, 1360000m, 6, "POP006-2", 23, "Màu Trắng" },
                    { 13, true, 1345000m, 7, "POP007-1", 23, "Màu Đen" },
                    { 14, true, 1495000m, 7, "POP007-2", 24, "Màu Trắng" },
                    { 15, true, 1480000m, 8, "POP008-1", 24, "Màu Đen" },
                    { 16, true, 1630000m, 8, "POP008-2", 25, "Màu Trắng" },
                    { 17, true, 1615000m, 9, "POP009-1", 25, "Màu Đen" },
                    { 18, true, 1765000m, 9, "POP009-2", 26, "Màu Trắng" },
                    { 19, true, 1750000m, 10, "POP010-1", 26, "Màu Đen" },
                    { 20, true, 1900000m, 10, "POP010-2", 27, "Màu Trắng" },
                    { 21, true, 1885000m, 11, "POP011-1", 27, "Màu Đen" },
                    { 22, true, 2035000m, 11, "POP011-2", 28, "Màu Trắng" },
                    { 23, true, 2020000m, 12, "POP012-1", 28, "Màu Đen" },
                    { 24, true, 2170000m, 12, "POP012-2", 29, "Màu Trắng" },
                    { 31, true, 2560000m, 16, "POP016-1", 32, "Màu Đen" },
                    { 32, true, 2710000m, 16, "POP016-2", 33, "Màu Trắng" },
                    { 33, true, 2695000m, 17, "POP017-1", 33, "Màu Đen" },
                    { 34, true, 2845000m, 17, "POP017-2", 34, "Màu Trắng" },
                    { 35, true, 2830000m, 18, "POP018-1", 34, "Màu Đen" },
                    { 36, true, 2980000m, 18, "POP018-2", 35, "Màu Trắng" },
                    { 37, true, 2965000m, 19, "POP019-1", 35, "Màu Đen" },
                    { 38, true, 3115000m, 19, "POP019-2", 36, "Màu Trắng" },
                    { 39, true, 3100000m, 20, "POP020-1", 36, "Màu Đen" },
                    { 40, true, 3250000m, 20, "POP020-2", 37, "Màu Trắng" },
                    { 41, true, 3235000m, 21, "POP021-1", 37, "Màu Đen" },
                    { 42, true, 3385000m, 21, "POP021-2", 38, "Màu Trắng" },
                    { 43, true, 3370000m, 22, "POP022-1", 38, "Màu Đen" },
                    { 44, true, 3520000m, 22, "POP022-2", 39, "Màu Trắng" },
                    { 45, true, 3505000m, 23, "POP023-1", 39, "Màu Đen" },
                    { 46, true, 3655000m, 23, "POP023-2", 40, "Màu Trắng" },
                    { 47, true, 3640000m, 24, "POP024-1", 40, "Màu Đen" },
                    { 48, true, 3790000m, 24, "POP024-2", 41, "Màu Trắng" },
                    { 55, true, 4180000m, 28, "POP028-1", 44, "Màu Đen" },
                    { 56, true, 4330000m, 28, "POP028-2", 15, "Màu Trắng" },
                    { 57, true, 4315000m, 29, "POP029-1", 15, "Màu Đen" },
                    { 58, true, 4465000m, 29, "POP029-2", 16, "Màu Trắng" },
                    { 59, true, 4450000m, 30, "POP030-1", 16, "Màu Đen" },
                    { 60, true, 4600000m, 30, "POP030-2", 17, "Màu Trắng" },
                    { 61, true, 4585000m, 31, "POP031-1", 17, "Màu Đen" },
                    { 62, true, 4735000m, 31, "POP031-2", 18, "Màu Trắng" },
                    { 63, true, 4720000m, 32, "POP032-1", 18, "Màu Đen" },
                    { 64, true, 4870000m, 32, "POP032-2", 19, "Màu Trắng" },
                    { 65, true, 4855000m, 33, "POP033-1", 19, "Màu Đen" },
                    { 66, true, 5005000m, 33, "POP033-2", 20, "Màu Trắng" },
                    { 67, true, 4990000m, 34, "POP034-1", 20, "Màu Đen" },
                    { 68, true, 5140000m, 34, "POP034-2", 21, "Màu Trắng" },
                    { 69, true, 5125000m, 35, "POP035-1", 21, "Màu Đen" },
                    { 70, true, 5275000m, 35, "POP035-2", 22, "Màu Trắng" },
                    { 71, true, 5260000m, 36, "POP036-1", 22, "Màu Đen" },
                    { 72, true, 5410000m, 36, "POP036-2", 23, "Màu Trắng" },
                    { 79, true, 800000m, 40, "POP040-1", 26, "Màu Đen" },
                    { 80, true, 950000m, 40, "POP040-2", 27, "Màu Trắng" },
                    { 81, true, 935000m, 41, "POP041-1", 27, "Màu Đen" },
                    { 82, true, 1085000m, 41, "POP041-2", 28, "Màu Trắng" },
                    { 83, true, 1070000m, 42, "POP042-1", 28, "Màu Đen" },
                    { 84, true, 1220000m, 42, "POP042-2", 29, "Màu Trắng" },
                    { 85, true, 1205000m, 43, "POP043-1", 29, "Màu Đen" },
                    { 86, true, 1355000m, 43, "POP043-2", 30, "Màu Trắng" },
                    { 87, true, 1340000m, 44, "POP044-1", 30, "Màu Đen" },
                    { 88, true, 1490000m, 44, "POP044-2", 31, "Màu Trắng" },
                    { 89, true, 1475000m, 45, "POP045-1", 31, "Màu Đen" },
                    { 90, true, 1625000m, 45, "POP045-2", 32, "Màu Trắng" },
                    { 91, true, 1610000m, 46, "POP046-1", 32, "Màu Đen" },
                    { 92, true, 1760000m, 46, "POP046-2", 33, "Màu Trắng" },
                    { 93, true, 1745000m, 47, "POP047-1", 33, "Màu Đen" },
                    { 94, true, 1895000m, 47, "POP047-2", 34, "Màu Trắng" },
                    { 95, true, 1880000m, 48, "POP048-1", 34, "Màu Đen" },
                    { 96, true, 2030000m, 48, "POP048-2", 35, "Màu Trắng" },
                    { 103, true, 2420000m, 52, "POP052-1", 38, "Màu Đen" },
                    { 104, true, 2570000m, 52, "POP052-2", 39, "Màu Trắng" },
                    { 105, true, 2555000m, 53, "POP053-1", 39, "Màu Đen" },
                    { 106, true, 2705000m, 53, "POP053-2", 40, "Màu Trắng" },
                    { 107, true, 2690000m, 54, "POP054-1", 40, "Màu Đen" },
                    { 108, true, 2840000m, 54, "POP054-2", 41, "Màu Trắng" },
                    { 109, true, 2825000m, 55, "POP055-1", 41, "Màu Đen" },
                    { 110, true, 2975000m, 55, "POP055-2", 42, "Màu Trắng" },
                    { 111, true, 2960000m, 56, "POP056-1", 42, "Màu Đen" },
                    { 112, true, 3110000m, 56, "POP056-2", 43, "Màu Trắng" },
                    { 113, true, 3095000m, 57, "POP057-1", 43, "Màu Đen" },
                    { 114, true, 3245000m, 57, "POP057-2", 44, "Màu Trắng" },
                    { 115, true, 3230000m, 58, "POP058-1", 44, "Màu Đen" },
                    { 116, true, 3380000m, 58, "POP058-2", 15, "Màu Trắng" },
                    { 117, true, 3365000m, 59, "POP059-1", 15, "Màu Đen" },
                    { 118, true, 3515000m, 59, "POP059-2", 16, "Màu Trắng" },
                    { 119, true, 3500000m, 60, "POP060-1", 16, "Màu Đen" },
                    { 120, true, 3650000m, 60, "POP060-2", 17, "Màu Trắng" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 60);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 61);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 62);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 63);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 64);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 65);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 66);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 67);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 68);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 69);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 70);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 71);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 72);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 73);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 74);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 75);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 76);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 77);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 78);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 79);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 80);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 81);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 82);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 83);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 84);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 85);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 86);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 87);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 88);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 89);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 90);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 91);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 92);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 93);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 94);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 95);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 96);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 97);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 98);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 99);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 100);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 102);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 103);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 104);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 105);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 106);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 107);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 108);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 109);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 110);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 111);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 112);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 113);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 114);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 115);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 116);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 117);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 118);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 119);

            migrationBuilder.DeleteData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 120);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 60);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 61);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 62);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 63);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 64);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 65);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 66);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 67);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 68);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 69);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 70);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 71);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 72);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 73);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 74);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 75);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 76);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 77);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 78);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 79);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 80);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 81);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 82);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 83);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 84);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 85);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 86);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 87);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 88);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 89);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 90);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 91);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 92);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 93);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 94);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 95);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 96);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 97);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 98);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 99);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 100);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 102);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 103);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 104);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 105);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 106);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 107);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 108);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 109);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 110);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 111);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 112);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 113);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 114);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 115);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 116);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 117);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 118);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 119);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 120);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 60);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "2c6aa404-81a8-4acd-8a07-cb5c693c7785");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "2f111a3b-cc45-4109-a947-dd8f5afb6dad");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "bc16f136-469e-443e-9dad-6f33d5e7a326");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-001",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8a6f1d22-38c7-4e72-8a95-6ce5c4bc935f", "AQAAAAIAAYagAAAAEAIUG7Ki67vsQYPgMbzZJ47oitL6bkFjmqI5+xFVDJT7mr9k9zvM1eZ0HcaLRmzGaA==", "ade20d89-0238-43d9-83ff-a1b388be8a7a" });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Thương hiệu cầu lông hàng đầu từ Nhật Bản", "Yonex" });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "Description", "Name", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Thương hiệu cầu lông từ Đài Loan", "Victor", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "Description", "Name", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Thương hiệu cầu lông từ Trung Quốc", "Li-Ning", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Các loại vợt cầu lông", "Vợt cầu lông" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "Description", "Name", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Giày chơi cầu lông", "Giày cầu lông", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "Description", "Name", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Trang phục chơi cầu lông", "Quần áo cầu lông", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "Description", "Name", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Các phụ kiện liên quan đến cầu lông", "Phụ kiện cầu lông", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "Description", "Name", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Cầu và các vật liệu liên quan", "Cầu cầu lông", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Price", "SKU", "StockQuantity", "VariantLabel" },
                values: new object[] { 2500000m, "YONEX-ASTROX-001", 10, null });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Price", "ProductId", "SKU", "StockQuantity", "VariantLabel" },
                values: new object[] { 1200000m, 2, "VICTOR-A830-001", 15, null });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Price", "ProductId", "SKU", "StockQuantity", "VariantLabel" },
                values: new object[] { 350000m, 3, "LINING-AO-001", 20, null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Description", "Name", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Vợt cầu lông cao cấp", "Vợt Yonex Astrox", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "Description", "Name", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Giày cầu lông bền bỉ", "Giày Victor A830", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "Description", "Name", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Áo thể thao cầu lông", "Áo Li-Ning", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) });
        }
    }
}
