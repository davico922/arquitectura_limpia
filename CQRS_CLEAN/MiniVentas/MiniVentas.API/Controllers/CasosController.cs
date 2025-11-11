using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniVentas.API.Helpers;
using MiniVentas.Application.DTOs.Casos.Request;
using MiniVentas.Application.DTOs.Casos.Response;
using MiniVentas.Application.DTOs.Productos.Request;
using MiniVentas.Application.Features.Productos.Commands;
using MiniVentas.Application.Features.VisivaApis.Queries;
using MiniVentas.Domain.Entities.Util;

namespace MiniVentas.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CasosController : ControllerBase
    {
        private readonly ISender _sender;

        public CasosController(ISender sender)
        {
            _sender = sender;
        }
        [HttpPost]
        [Route("ListarCasos")]
        public async Task<ActionResult<ApiResponse<ListadoSolicitudesFilterResponseDto>>> ListarCasos([FromBody] ListadoSolicitudesFilterRequestDto request)
        {
          
            ValidationHelper.ValidateModel(ModelState);

            //peticion 

            var command = new GetCasosQuery(request);
            var result = await _sender.Send(command);
            return Ok(result);
        }
    }
}
