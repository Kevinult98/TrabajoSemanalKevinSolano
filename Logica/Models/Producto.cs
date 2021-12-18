using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica.Models
{
   public class Producto : ICrudBase
    {
        public int CodigoProducto { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal PorcentajeImpuesto { get; set; }
        public decimal PrecioCompra { get; set; }
        public decimal PrecioVenta { get; set; }
        public int Stock { get; set; }
        public byte Estado { get; set; }
        public CategoriaProducto CodigoCategoria { get; set; }

        public Producto()
        {
            CodigoCategoria = new CategoriaProducto();
        }

        DataTable Listar()
        {
            DataTable R = new DataTable();
            return R;
        }

        public bool Agregar()
        {
            bool R = false;
            return R;
        }

        public bool Editar()
        {
            bool R = false;
            return R;
        }

        public bool Eliminar()
        {
            bool R = false;
            return R;
        }
    }
}
