using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MiniVentas.Application.DTOs.Productos.Response;
using MiniVentas.Application.Features.Productos.Queries;
using MiniVentas.Domain.Entities.Util;

namespace MiniVentas.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly ISender _sender;

        public ProductosController(ISender sender)
        {
                _sender=sender;
        }


        [HttpGet]
        [Route("GetProductByID/{Id_Producto}")]
        public async Task<ActionResult<ApiResponse<List<ListadoProductoResponseDTO>>>> GetByID(int Id_Producto)
        {
            var query = new GetAllProductsQuery(Id_Producto);
            var result = await _sender.Send(query);

            // la respuesta se puede usar de las 2 formas 

            //var response = new ApiResponse<List<ListadoProductoResponseDTO>>()
            //{
            //    Success = result != null && result.Any(),
            //    Message = result.Any() ? "Productos encontrados." : "No se encontraron productos.",
            //    Data = result
            //};

            //return Ok(response);

            if (result == null || !result.Any())
                return Ok(new ApiResponse<List<ListadoProductoResponseDTO>>(false, "No se encontraron productos."));

            return Ok(new ApiResponse<List<ListadoProductoResponseDTO>>(true, "Productos encontrados.", result));

        }

    }
}
