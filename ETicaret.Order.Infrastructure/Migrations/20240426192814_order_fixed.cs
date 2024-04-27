using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ETicaret.Order.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class order_fixed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductId",
                schema: "orders",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ProductName",
                schema: "orders",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Quantity",
                schema: "orders",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "UnitPrice",
                schema: "orders",
                table: "Orders");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                schema: "orders",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ProductName",
                schema: "orders",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                schema: "orders",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "UnitPrice",
                schema: "orders",
                table: "Orders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
