using Microsoft.EntityFrameworkCore.Migrations;

namespace Temp.DataAccess.Migrations
{
    public partial class updateTableProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CartDetail_ProductId",
                table: "CartDetail");

            migrationBuilder.CreateIndex(
                name: "IX_CartDetail_ProductId",
                table: "CartDetail",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CartDetail_ProductId",
                table: "CartDetail");

            migrationBuilder.CreateIndex(
                name: "IX_CartDetail_ProductId",
                table: "CartDetail",
                column: "ProductId",
                unique: true);
        }
    }
}
