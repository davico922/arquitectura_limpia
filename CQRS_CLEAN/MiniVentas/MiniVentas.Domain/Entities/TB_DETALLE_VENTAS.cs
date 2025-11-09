using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniVentas.Domain.Entities
{
    public class TB_DETALLE_VENTAS
    {

        public int ID_DETALLE_VENTAS { get; set; }
        public int ID_VENTA { get; set; }
        public int ID_PRODUCTO { get; set; }
        public DateTime FECHA_REGISTRO { get; set; }
        public int CANTIDAD { get; set; }
    }
}
