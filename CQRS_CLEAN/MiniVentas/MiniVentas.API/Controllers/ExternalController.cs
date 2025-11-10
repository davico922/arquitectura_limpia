using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniVentas.API.Helpers;
using MiniVentas.Application.External.DTOs.FakeStoreProducto.CreateProducto.Request;
using MiniVentas.Application.External.DTOs.FakeStoreProducto.CreateProducto.Response;
using MiniVentas.Application.External.DTOs.FakeStoreProducto.GetProductoById.Response;
using MiniVentas.Application.External.Interfaces.FakeStoreProducto;
using MiniVentas.Domain.Entities.Util;

namespace MiniVentas.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ExternalController : ControllerBase
    {

        private readonly IFakeStoreProducto _fakeStoreProducto;

        public ExternalController(IFakeStoreProducto fakeStoreProducto)
        {
            _fakeStoreProducto = fakeStoreProducto;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<FakeStoreProductByIdResponseDto>>> GetProductoById(int id)
        {
            ValidationHelper.ValidateModel(ModelState);

            var response = new ApiResponse<FakeStoreProductByIdResponseDto>();
            var producto = await _fakeStoreProducto.GetProductByIdAsync(id);

                if (producto == null)
                {
                    response.Success = false;
                    response.Message = "No se encontró el producto solicitado.";
                    return NotFound(response);
                }

                response.Data = producto;
                response.Success = true;
                response.Message = "Producto obtenido correctamente.";

                return Ok(response);
           
        }

        [HttpPost]
        [Route("Create")]
        public async Task<ActionResult<ApiResponse<FakeStoreCreateProductoResponseDto>>> CreateProduct([FromBody] FakeStoreCreateProductoRequestDto request)
        {
            ValidationHelper.ValidateModel(ModelState);

            var product = await _fakeStoreProducto.CreateProductoAsync(request);

            var response = new ApiResponse<FakeStoreCreateProductoResponseDto>
            {
                Success = true,
                Message = "Producto creado correctamente en FakeStore API.",
                Data = product
            };

            return Ok(response);
        }

    }
}
