using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniVentas.Application.External.DTOs.VisivaApis.Token.Response
{
    public class VisivaAuthResponseDto
    {
        public int Estado { get; set; }
        public string Mensaje { get; set; }
        public VisivaAuthDetalle Detalle { get; set; }
    }

    public class VisivaAuthDetalle
    {
        public string usuario { get; set; }
        public string token { get; set; }
    }
}
