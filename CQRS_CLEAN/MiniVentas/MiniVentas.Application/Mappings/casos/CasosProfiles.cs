using AutoMapper;
using MiniVentas.Application.DTOs.Casos.Response;
using MiniVentas.Application.External.DTOs.VisivaApis.VisivaCasos.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniVentas.Application.Mappings.casos
{
    public class CasosProfiles : Profile
    {

        public CasosProfiles()
        {
            CreateMap<VisivaCasosResponseDto.Caso, ListadoSolicitudesFilterResponseDto.Caso>();
        }
    }
}
