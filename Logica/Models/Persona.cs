using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica.Models
{
    class Persona : ICrudBase
    {
        public int CodigoPersona { get; set; }
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Direccion { get; set; }
        public TipoPersona CodigoTipoPersona { get; set; }

        public Persona()
        {
            CodigoTipoPersona = new TipoPersona();
        }
        public bool Agregar()
        {
            throw new NotImplementedException();
        }

        public bool Editar()
        {
            bool R = false;
            return R;
        }

        public bool Eliminar()
        {
            throw new NotImplementedException();
        }
        //Ahora agregamos las funciones que no estaban en interfaces
        bool ConsultarPorID( int ID)
        {
            bool R = false;
            return R;
        }

        bool ConsultarPorCedula(string Cedula)
        {
            bool R = false;
            return R;
        }
        public DataTable ListarActivos()
        {
            DataTable R = new DataTable();
            return R;
        }
        public DataTable ListarInactivos()
        {
            DataTable R = new DataTable();
            return R;
        }

    }
}
