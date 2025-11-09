using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniVentas.Domain.Entities
{
    public class TB_VENTAS
    {

        public int ID_VENTA { get; set; }
        public int ID_CLIENTE { get; set; }
        public string NRO_DOCUMENTO_VENTA { get; set; } = string.Empty;
        public decimal TOTAL { get; set; }
        public int ID_ESTADO { get; set; }
        public int ID_USUARIO_REGISTRO { get; set; }
        public DateTime FECHA_VENTA { get; set; }

    }
}
