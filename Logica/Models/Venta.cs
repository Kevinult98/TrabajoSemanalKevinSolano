using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica.Models
{
    class Venta
    {
        public int CodigoVenta { get; set; }
        public Usuario CodigoUsuario { get; set; }
        public Persona CodigoPersona { get; set; }
        public DateTime FechaVenta { get; set; }
        public decimal ImpuetoTotal { get; set; }
        public decimal Subtotal { get; set; }
        public decimal TotalVenta { get; set; }

        public Venta()
        {
            CodigoUsuario = new Usuario();
            CodigoPersona = new Persona();
        }
        DataTable Listar()
        {
            DataTable R = new DataTable();
            return R;
        }
    }
}
