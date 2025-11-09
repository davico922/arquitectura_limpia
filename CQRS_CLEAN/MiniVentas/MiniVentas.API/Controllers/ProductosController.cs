using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MiniVentas.API.Helpers;
using MiniVentas.Application.DTOs.Productos.Request;
using MiniVentas.Application.DTOs.Productos.Response;
using MiniVentas.Application.Features.Productos.Commands;
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
        //tbm funciona de manera directa LA UNICA DIFERENCIA ES QUE EL OBJETO SALE CON EL NOMBRE DE PRODUCTO
        //[HttpPost]
        //[Route("Create")]
        //public async Task<ActionResult<ApiResponse<int>>> Create(CreateProductCommand command)
        //{
        //    var result = await _sender.Send(command);

        //    return Ok(result);
        //}

        //manera recomendada
        [HttpPost]
        [Route("Create")]
        public async Task<ActionResult<ApiResponse<int>>> CrearProducto([FromBody] MantenimientoProductoRequestDto request)
        {
            ////se usa para validar que los datos estan correctos si no se va al middleware que control los errores de forma global
            //if (!ModelState.IsValid)
            //{
            //    // Lanzamos una excepción que tu middleware ya sabe manejar
            //    throw new ArgumentException("Los datos de entrada son inválidos. " +
            //                                string.Join("; ", ModelState.Values
            //                                .SelectMany(v => v.Errors)
            //                                .Select(e => e.ErrorMessage)));
            //}

            //helper de validacion de datos es lo mismo del codigo de arriba
            ValidationHelper.ValidateModel(ModelState);

            //peticion 

            var command = new CreateProductCommand(request);
            var result = await _sender.Send(command);
            return Ok(result);
        }
    }
}
