using AutoMapper;
using MiniVentas.Application.DTOs.Logs.Request;
using MiniVentas.Application.DTOs.Productos.Request;
using MiniVentas.Domain.Entities.Logs;
using MiniVentas.Domain.Models.Productos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniVentas.Application.Mappings.Logs;

public class LogProfiles :Profile
{
    public LogProfiles()
    {
        CreateMap<LogTransaccion, CreateLogsTransaccionRequestDto >().ReverseMap();
    }

}
