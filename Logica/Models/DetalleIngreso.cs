using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica.Models
{
    class DetalleIngreso
    {
        public int CodigoDetalleIngreso { get; set; }
        public Producto CodigoProducto { get; set; }

        public Ingreso CodigoIngreso { get; set; }
        public decimal PrecioCompra { get; set; }
        public int Cantidad { get; set; }
        public decimal Impuesto { get; set; }
        public decimal Total { get; set; }

        public DetalleIngreso()
        {
            CodigoProducto = new Producto();
            CodigoIngreso = new Ingreso();
        }
        DataTable Listar()
        {
            DataTable R = new DataTable();
            return R;
        }
    }
}
