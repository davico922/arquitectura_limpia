using MiniVentas.Domain.Entities.Logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniVentas.Domain.Interfaces;

public interface ILogRepository
{
    Task InsertarLog(LogTransaccion log);

}
