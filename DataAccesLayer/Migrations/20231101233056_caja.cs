using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccesLayer.Migrations
{
    /// <inheritdoc />
    public partial class caja : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Producto_Ingrediente_Ingrediente_Id_Ingrediente",
                table: "Producto_Ingrediente");

            migrationBuilder.DropForeignKey(
                name: "FK_Producto_Ingrediente_Producto_Id_Producto",
                table: "Producto_Ingrediente");

            migrationBuilder.RenameColumn(
                name: "Id_Ingrediente",
                table: "Producto_Ingrediente",
                newName: "id_Ingrediente");

            migrationBuilder.RenameColumn(
                name: "Id_Producto",
                table: "Producto_Ingrediente",
                newName: "id_Producto");

            migrationBuilder.RenameIndex(
                name: "IX_Producto_Ingrediente_Id_Ingrediente",
                table: "Producto_Ingrediente",
                newName: "IX_Producto_Ingrediente_id_Ingrediente");

            migrationBuilder.RenameColumn(
                name: "Observaciones",
                table: "Pedido_Producto",
                newName: "observaciones");

            migrationBuilder.RenameColumn(
                name: "Id_Producto",
                table: "Pedido_Producto",
                newName: "id_Producto");

            migrationBuilder.RenameColumn(
                name: "Id_Pedido",
                table: "Pedido_Producto",
                newName: "id_Pedido");

            migrationBuilder.RenameColumn(
                name: "IdPedidoProducto",
                table: "Pedido_Producto",
                newName: "idPedidoProducto");

            migrationBuilder.RenameColumn(
                name: "Registro_Activo",
                table: "Mesa",
                newName: "registro_Activo");

            migrationBuilder.RenameColumn(
                name: "PrecioTotal",
                table: "Mesa",
                newName: "precioTotal");

            migrationBuilder.RenameColumn(
                name: "EnUso",
                table: "Mesa",
                newName: "enUso");

            migrationBuilder.RenameColumn(
                name: "Id_Mesa",
                table: "Mesa",
                newName: "id_Mesa");

            migrationBuilder.RenameColumn(
                name: "TotalPrecios",
                table: "Cajas",
                newName: "totalPrecios");

            migrationBuilder.RenameColumn(
                name: "Fecha",
                table: "Cajas",
                newName: "fecha");

            migrationBuilder.RenameColumn(
                name: "Estado",
                table: "Cajas",
                newName: "estado");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Cajas",
                newName: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Producto_Ingrediente_Ingrediente_id_Ingrediente",
                table: "Producto_Ingrediente",
                column: "id_Ingrediente",
                principalTable: "Ingrediente",
                principalColumn: "id_Ingrediente",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Producto_Ingrediente_Producto_id_Producto",
                table: "Producto_Ingrediente",
                column: "id_Producto",
                principalTable: "Producto",
                principalColumn: "id_Producto",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Producto_Ingrediente_Ingrediente_id_Ingrediente",
                table: "Producto_Ingrediente");

            migrationBuilder.DropForeignKey(
                name: "FK_Producto_Ingrediente_Producto_id_Producto",
                table: "Producto_Ingrediente");

            migrationBuilder.RenameColumn(
                name: "id_Ingrediente",
                table: "Producto_Ingrediente",
                newName: "Id_Ingrediente");

            migrationBuilder.RenameColumn(
                name: "id_Producto",
                table: "Producto_Ingrediente",
                newName: "Id_Producto");

            migrationBuilder.RenameIndex(
                name: "IX_Producto_Ingrediente_id_Ingrediente",
                table: "Producto_Ingrediente",
                newName: "IX_Producto_Ingrediente_Id_Ingrediente");

            migrationBuilder.RenameColumn(
                name: "observaciones",
                table: "Pedido_Producto",
                newName: "Observaciones");

            migrationBuilder.RenameColumn(
                name: "id_Producto",
                table: "Pedido_Producto",
                newName: "Id_Producto");

            migrationBuilder.RenameColumn(
                name: "id_Pedido",
                table: "Pedido_Producto",
                newName: "Id_Pedido");

            migrationBuilder.RenameColumn(
                name: "idPedidoProducto",
                table: "Pedido_Producto",
                newName: "IdPedidoProducto");

            migrationBuilder.RenameColumn(
                name: "registro_Activo",
                table: "Mesa",
                newName: "Registro_Activo");

            migrationBuilder.RenameColumn(
                name: "precioTotal",
                table: "Mesa",
                newName: "PrecioTotal");

            migrationBuilder.RenameColumn(
                name: "enUso",
                table: "Mesa",
                newName: "EnUso");

            migrationBuilder.RenameColumn(
                name: "id_Mesa",
                table: "Mesa",
                newName: "Id_Mesa");

            migrationBuilder.RenameColumn(
                name: "totalPrecios",
                table: "Cajas",
                newName: "TotalPrecios");

            migrationBuilder.RenameColumn(
                name: "fecha",
                table: "Cajas",
                newName: "Fecha");

            migrationBuilder.RenameColumn(
                name: "estado",
                table: "Cajas",
                newName: "Estado");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Cajas",
                newName: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Producto_Ingrediente_Ingrediente_Id_Ingrediente",
                table: "Producto_Ingrediente",
                column: "Id_Ingrediente",
                principalTable: "Ingrediente",
                principalColumn: "id_Ingrediente",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Producto_Ingrediente_Producto_Id_Producto",
                table: "Producto_Ingrediente",
                column: "Id_Producto",
                principalTable: "Producto",
                principalColumn: "id_Producto",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
