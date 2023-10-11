using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccesLayer.Migrations
{
    /// <inheritdoc />
    public partial class Migracion4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "observaciones",
                table: "Pedido");

            migrationBuilder.AddColumn<string>(
                name: "observaciones",
                table: "Pedido_Producto",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "observaciones",
                table: "Pedido_Producto");

            migrationBuilder.AddColumn<string>(
                name: "observaciones",
                table: "Pedido",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
