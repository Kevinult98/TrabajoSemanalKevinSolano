using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica.Models
{
   public class RolUsuario
    {
        public int CodigoRol { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public byte Estado { get; set; }
        public DataTable Listar()
        {
            DataTable R = new DataTable();

            Conexion MiConexion = new Conexion();

            R = MiConexion.DMLSelect("SPUsuarioRolListar");
            return R;
        }
        bool ConsultarPorID()
        {
            bool R = false;
            return R;
        }
    }
}
