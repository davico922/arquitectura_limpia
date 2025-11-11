using MiniVentas.Application.External.DTOs.VisivaApis.VisivaCasos.Request;
using MiniVentas.Application.External.DTOs.VisivaApis.VisivaCasos.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MiniVentas.Domain.Interfaces.External.VisivaApis
{
    public interface IVisivaApiRepository
    {

        Task<string> ObtenerTokenAsync();

        Task<VisivaCasosResponseDto> ObtenerCasosAsync( string token,VisivaCasosRequestDto request);
    }
}
