using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniVentas.Domain.Entities
{
    public class TB_PRODUCTOS
    {

        public int Id_Producto { get; set; }
        public string? Nombre { get; set; }
        public int Cantidad { get; set; }

        public decimal Precio { get; set; }

        public int Id_Estado { get; set; }

        public int Id_Usuario_Registro { get; set; }

        public DateTime Fecha_Registro { get; set; }

    }
}
