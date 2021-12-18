using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica.Models
{
    public class CategoriaProducto
    {
        public int CodigoCategoria { get; set; }
        public string Nombre { get; set; }

        public string Descripcion { get; set; }
        public byte Estado { get; set; }

        DataTable Listar()
        {
            DataTable R = new DataTable();
            return R;
        }

    }
}
