using Microsoft.EntityFrameworkCore.Migrations;

namespace Temp.DataAccess.Migrations
{
    public partial class updateOrderDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Sale_SaleId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_SaleId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "TotalMoney",
                table: "Cart");

            migrationBuilder.RenameColumn(
                name: "Percent",
                table: "Sale",
                newName: "PriceOld");

            migrationBuilder.RenameColumn(
                name: "SaleId",
                table: "Products",
                newName: "ProductType");

            migrationBuilder.AddColumn<int>(
                name: "PriceNow",
                table: "Sale",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Cart",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Sale_ProductId",
                table: "Sale",
                column: "ProductId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Sale_Products_ProductId",
                table: "Sale",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sale_Products_ProductId",
                table: "Sale");

            migrationBuilder.DropIndex(
                name: "IX_Sale_ProductId",
                table: "Sale");

            migrationBuilder.DropColumn(
                name: "PriceNow",
                table: "Sale");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Cart");

            migrationBuilder.RenameColumn(
                name: "PriceOld",
                table: "Sale",
                newName: "Percent");

            migrationBuilder.RenameColumn(
                name: "ProductType",
                table: "Products",
                newName: "SaleId");

            migrationBuilder.AddColumn<int>(
                name: "TotalMoney",
                table: "Cart",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_SaleId",
                table: "Products",
                column: "SaleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Sale_SaleId",
                table: "Products",
                column: "SaleId",
                principalTable: "Sale",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
