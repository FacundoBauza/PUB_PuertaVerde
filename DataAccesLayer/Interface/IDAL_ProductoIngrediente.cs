﻿using DataAccesLayer.Models;

namespace DataAccesLayer.Interface
{
    public interface IDAL_ProductoIngrediente
    {
        public bool ProductoIngrediente(int productoId, int ingredienteId);

        List<Ingredientes> getIngredientesProducto(int idProducto);
    }
}
