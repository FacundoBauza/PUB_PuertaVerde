using BusinessLayer.Interfaces;
using DataAccesLayer.Interface;
using DataAccesLayer.Models;

namespace BusinessLayer.Implementations
{
    public class B_Usuario : IB_Usuario
    {
        private IDAL_Usuario _dal;
        public B_Usuario(IDAL_Usuario dal)
        {
            _dal = dal;
        }

        //Agregar
        bool IB_Usuario.agregar_Usuario(Usuarios t)
        {
            return _dal.add_Usuario(t);
        }
    }
}
