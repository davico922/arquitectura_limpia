using AutoMapper;
using MediatR;
using MiniVentas.Application.DTOs.Casos.Request;
using MiniVentas.Application.DTOs.Casos.Response;
using MiniVentas.Application.External.DTOs.VisivaApis.Token.Request;
using MiniVentas.Application.External.DTOs.VisivaApis.VisivaCasos.Request;
using MiniVentas.Application.External.DTOs.VisivaApis.VisivaCasos.Response;
using MiniVentas.Domain.Entities.Util;
using MiniVentas.Domain.Interfaces;
using MiniVentas.Domain.Interfaces.External.VisivaApis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniVentas.Application.Features.VisivaApis.Queries;

public record GetCasosQuery (ListadoSolicitudesFilterRequestDto casos) :IRequest<ApiResponse<ListadoSolicitudesFilterResponseDto>>;

public record GetCasosQueryHandler : IRequestHandler<GetCasosQuery, ApiResponse<ListadoSolicitudesFilterResponseDto>>
{

    private readonly IVisivaApiRepository _visivaApiRepository;
    private readonly IMapper _mapper;
    public GetCasosQueryHandler(IVisivaApiRepository visivaApiRepository,IMapper mapper)
    {
        _visivaApiRepository = visivaApiRepository;
        _mapper=mapper;
        
    }

    public async Task<ApiResponse<ListadoSolicitudesFilterResponseDto>> Handle(GetCasosQuery request, CancellationToken cancellationToken)
    {
        var apiResponse = new ApiResponse<ListadoSolicitudesFilterResponseDto>();

        var token = await _visivaApiRepository.ObtenerTokenAsync();
        if (string.IsNullOrEmpty(token))
        {
            apiResponse.Success = false;
            apiResponse.Message = "No se pudo obtener el token de autenticación.";
            apiResponse.Data = null;
            return apiResponse;
        }

        var visivaRequest = new VisivaCasosRequestDto
        {
            emplid = request.casos.emplid,
            une = request.casos.une
        };

        var response = await _visivaApiRepository.ObtenerCasosAsync(token, visivaRequest);

        if (response == null || response.detalle == null || response.detalle.casos == null)
        {
            apiResponse.Success = false;
            apiResponse.Message = "No se encontraron casos.";
            apiResponse.Data = null;
            return apiResponse;
        }

        // 4️⃣ Aplicar el filtro (Estado)
        var filtrados = response.detalle.casos
            .Where(c => string.Equals(c.resultado__c, request.casos.Estado, StringComparison.OrdinalIgnoreCase))
            .ToList();


        var casosMapeados = _mapper.Map<List<ListadoSolicitudesFilterResponseDto.Caso>>(filtrados);


        // 5️⃣ Construir respuesta
        apiResponse.Success = true;
        apiResponse.Message = $"Se encontraron {filtrados.Count} registros con estado '{request.casos.Estado}'.";
        apiResponse.Data = new ListadoSolicitudesFilterResponseDto
        {
            casos = casosMapeados
        };

        return apiResponse;
    }

  
}
