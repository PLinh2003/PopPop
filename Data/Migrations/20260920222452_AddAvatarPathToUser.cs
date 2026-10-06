using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PopPop.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddAvatarPathToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AvatarPath",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "a1e69c29-c529-48c5-8fa9-0f1aae658cbf");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "5fec85c3-fc44-411d-ae46-6d2603fd4990");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "1fdf3824-9b1c-4cc1-818f-8e4463b91cd3");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-001",
                columns: new[] { "AvatarPath", "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { null, "ebaa3632-24cf-4a69-b55e-76ad6bac039e", "AQAAAAIAAYagAAAAEN9ibIjPnWJ4oDB50ElTSMl+B5MaRZ0duTY3q49mYvEkHhMI97705Ntdb0EulTNTMw==", "f28db10c-fb49-4fb4-92e1-c3dc10f10d8f" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvatarPath",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "5959cda7-5d54-4200-8df3-6771c2ee69e4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "6e49c3a1-08d8-47bc-a848-6de57141ce15");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "bffc462c-0341-492d-88a5-d0a406c50dfd");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-001",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d89898ad-17f5-4e25-920c-46424cb3e4f7", "AQAAAAIAAYagAAAAEGKxaK+AkZjtPPvrnJmNZCgwbYc8nAScwGFpuNeAh2zrc9h52prcZ2hsEY/IAASpFw==", "a518e691-f304-4b4f-a72f-ad889b489785" });
        }
    }
}
