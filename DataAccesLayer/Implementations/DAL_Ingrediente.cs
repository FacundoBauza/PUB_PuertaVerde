using DataAccesLayer.Interface;
using DataAccesLayer.Models;
using Domain.DT;
using Domain.Entidades;

namespace DataAccesLayer.Implementations
{
    public class DAL_Ingrediente : IDAL_Ingrediente
    {
        private readonly DataContext _db;
        public DAL_Ingrediente(DataContext db)
        {
            _db = db;
        }

        public MensajeRetorno Eliminar_Ingredente(int id)
        {
            MensajeRetorno msg = new();
            if (_db.Productos_Ingredientes.Where(p=>p.id_Ingrediente==id).Any())
            {
                msg.status = false;
                msg.mensaje = "El ingrediente pertenece a un producto por lo que no es posible eliminarlo";
            }
            else
            {
                try
                {
                    Ingredientes? ing = _db.Ingredientes. FirstOrDefault(x => x.id_Ingrediente == id);
                    if (ing != null) { 
                    _db.Ingredientes.Remove(ing);
                    msg.status = true;
                    msg.mensaje = "El ingrediente fue eliminado";
                    _db.SaveChanges();
                    }
                }
                catch {
                    msg.Exepcion_no_Controlada();
                }
            }
            return msg;
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
                try
                {
                    // Modifica las propiedades del ingrediente.
                    ingredienteEncontrado.stock = dti.stock;
                    ingredienteEncontrado.id_Categoria = dti.id_Categoria;
                    ingredienteEncontrado.nombre = dti.nombre;
                    // Guarda los cambios en la base de datos.
                    _db.Update(ingredienteEncontrado);
                    _db.SaveChanges();
                    return true;
                }
                catch { }
            }
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
