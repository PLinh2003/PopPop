using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PopPop.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddExchangeFulfillment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReplacementProductVariantId",
                table: "AfterSalesRequests",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReplacementSKU",
                table: "AfterSalesRequests",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AfterSalesRequests_ReplacementProductVariantId",
                table: "AfterSalesRequests",
                column: "ReplacementProductVariantId");

            migrationBuilder.AddForeignKey(
                name: "FK_AfterSalesRequests_ProductVariants_ReplacementProductVariantId",
                table: "AfterSalesRequests",
                column: "ReplacementProductVariantId",
                principalTable: "ProductVariants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AfterSalesRequests_ProductVariants_ReplacementProductVariantId",
                table: "AfterSalesRequests");

            migrationBuilder.DropIndex(
                name: "IX_AfterSalesRequests_ReplacementProductVariantId",
                table: "AfterSalesRequests");

            migrationBuilder.DropColumn(
                name: "ReplacementProductVariantId",
                table: "AfterSalesRequests");

            migrationBuilder.DropColumn(
                name: "ReplacementSKU",
                table: "AfterSalesRequests");
        }
    }
}
