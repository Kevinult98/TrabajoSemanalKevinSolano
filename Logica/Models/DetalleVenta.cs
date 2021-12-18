using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica.Models
{
    class DetalleVenta
    {
        public int CodigoDetalleVenta { get; set; }
        public Venta CodigoVenta { get; set; }

        public Producto CodigoProducto { get; set; }

        public decimal PrecioVenta { get; set; }

        public int Cantidad { get; set; }
        public decimal Impuesto { get; set; }

        public decimal Total { get; set; }

        public DetalleVenta()
        {
            CodigoVenta = new Venta();
            CodigoProducto = new Producto();
        }

        DataTable Listar()
        {
            DataTable R = new DataTable();
            return R;
        }
    }
}
