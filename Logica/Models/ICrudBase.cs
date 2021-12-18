using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica.Models
{
    interface ICrudBase
    {
        bool Agregar();
        bool Editar();
        bool Eliminar();
    }
}
