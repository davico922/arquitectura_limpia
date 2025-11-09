using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniVentas.Application.DTOs.Logs.Request
{
    internal class CreateLogsTransaccionRequestDto
    {
        public int IdUsuario { get; set; }
        public string? Accion { get; set; }
        public string? Request { get; set; }
        public string? Entidad { get; set; }
        public string? Resultado { get; set; }
      

    }
}
