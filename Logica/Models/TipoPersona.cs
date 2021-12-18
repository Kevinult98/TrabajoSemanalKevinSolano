using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica.Models
{
    class TipoPersona
    {
        public int CodigoTipoPersona { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public byte Estado { get; set; }
        public DataTable Listar()
        {
            DataTable R = new DataTable();
            return R;
        }
        public bool ConsultarPorID()
        {
            bool R = false;
            return R;
        }

        public bool ConsultarPorNombre()
        {
            bool R = false;
            return R;
        }


    }
}
