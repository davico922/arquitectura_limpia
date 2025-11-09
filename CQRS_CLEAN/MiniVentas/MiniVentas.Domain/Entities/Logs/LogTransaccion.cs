using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniVentas.Domain.Entities.Logs;

public class LogTransaccion
{
    public int Id { get; set; }
    public int IdUsuario { get; set; }
    public string? Accion { get; set; }
    public string? Request { get; set; }
    public string? Entidad { get; set; }
    public string? Resultado { get; set; }
    public DateTime Fecha { get; set; } = DateTime.Now;
 

}
