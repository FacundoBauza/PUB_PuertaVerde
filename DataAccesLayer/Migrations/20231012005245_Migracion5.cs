using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DataAccesLayer.Migrations
{
    /// <inheritdoc />
    public partial class Migracion5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Pedido_Producto",
                table: "Pedido_Producto");

            migrationBuilder.AddColumn<int>(
                name: "idPedidoProducto",
                table: "Pedido_Producto",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pedido_Producto",
                table: "Pedido_Producto",
                column: "idPedidoProducto");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Pedido_Producto",
                table: "Pedido_Producto");

            migrationBuilder.DropColumn(
                name: "idPedidoProducto",
                table: "Pedido_Producto");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pedido_Producto",
                table: "Pedido_Producto",
                columns: new[] { "id_Pedido", "id_Producto" });
        }
    }
}
