using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entidades
{
    public class MensajeRetorno
    {
        public bool status { get; set; }
        public string mensaje { get; set; }

#pragma warning disable CS8618
        public MensajeRetorno()
        {
        }

        public void Exepcion_no_Controlada()
        {
            mensaje = "Exepción no controlada";
            status = false;
        }
        public void El_Objeto_enviado_es_Nulo()
        {
            mensaje = "El objeto enviado es nulo";
            status = false;
        }
        public void Objeto_Nulo()
        {
            mensaje = "Objeto Nulo";
            status = false;
        }

        //Categoria
        public void La_Categoria_se_guardo_Correctamente()
        {
            mensaje = "La Categoria se guardo correctamente";
            status = true;
        }

        public void Ya_existe_una_Categoria_con_el_Nombre_ingresado()
        {
            mensaje = "Ya existe una Categoria con el Nombre ingresado";
            status = false;
        }

        public void La_Categoria_se_quito_Correctamente()
        {
            mensaje = "La Categoria se a dado de baja correctamente";
            status = true;
        }

        public void El_Cliente_se_guardo_Correctamente()
        {
            mensaje = "El Cliente se guardo correctamente";
            status = true;
        }

        public void Ya_existe_un_Cliente_con_los_datos_ingresado()
        {
            mensaje = "Ya esxiste un cliente con los datos ingresados";
            status = false;
        }

        //Ingrediente
        public void Ya_existe_un_Ingrediente_con_el_Nombre_ingresado()
        {
            mensaje = "Ya existe un Ingrediente con el Nombre ingresado";
            status = false;
        }

        public void El_Ingrediente_se_guardo_Correctamente()
        {
            mensaje = "El Ingrediente se guardo correctamente";
            status = true;
        }

        public void El_Ingrediente_se_modifico_Correctamente()
        {
            mensaje = "El Ingrediente se modifico correctamente";
            status = true;
        }

        public void No_existe_un_Cliente_con_los_datos_ingresado()
        {
            mensaje = "No existe un Cliente con los datos aportados";
            status = false;
        }

        public void El_Cliente_se_actualizo_Correctamente()
        {
            mensaje = "El Cliente se actualizo correctamente";
            status = true;
        }

        public void El_Cliente_se_quito_Correctamente()
        {
            mensaje = "El Cliente se dio de baja correctamente";
            status = true;
        }

}
}
