using DataAccesLayer.Interface;
using DataAccesLayer.Models;
using Domain.DT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace DataAccesLayer.Implementations
{
    public class DAL_Ingrediente: IDAL_Ingrediente
    {
        private readonly DataContext _db;
        public DAL_Ingrediente(DataContext db)
        {
            _db = db;
        }

        public List<Ingredientes> getIngrediente()
        {
            return _db.Ingredientes.Select(x => x.GetIngrediente()).ToList();
        }

        public bool modificar_Ingrediente(DTIngrediente dti)
        {
            // Utiliza SingleOrDefault() para buscar un ingrediente por nombre.
            var ingredienteEncontrado = _db.Ingredientes.SingleOrDefault(i => i.id_Ingrediente == dti.id_Ingrediente);
            if (ingredienteEncontrado != null)
            {
                // Modifica las propiedades del ingrediente.
                ingredienteEncontrado.stock = dti.stock;
                // Guarda los cambios en la base de datos.
                _db.SaveChanges();
                
                return true;
            }
            else
                return false;
        }

        public bool set_Ingrediente(DTIngrediente dti)
        {
            //Castea el DT en tipo Ingrediente
            Ingredientes aux = Ingredientes.SetIngrediente(dti);
            try
            {
                //Agrega el Ingrediente
                _db.Ingredientes.Add(aux);

                // Guarda los cambios en la base de datos.
                _db.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
