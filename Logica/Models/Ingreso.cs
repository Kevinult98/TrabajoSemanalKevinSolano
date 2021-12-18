using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica.Models
{
    class Ingreso
    {
        public int CodigoIngreso { get; set; }
        public  DateTime FechaIngreso { get; set; }
        public decimal Subtotal { get; set; }
        public decimal TotalImpuesto { get; set; }
        public decimal TotalIngreso { get; set; }
        public Persona CodigoPersona { get; set; }
        public Usuario CodigoUsuario { get; set; }
        public Ingreso()
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
