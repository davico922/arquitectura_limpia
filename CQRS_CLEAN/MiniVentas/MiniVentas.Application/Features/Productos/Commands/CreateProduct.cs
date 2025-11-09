using AutoMapper;
using MediatR;
using MiniVentas.Application.DTOs.Productos.Request;
using MiniVentas.Application.DTOs.Productos.Response;
using MiniVentas.Domain.Entities.Logs;
using MiniVentas.Domain.Entities.Util;
using MiniVentas.Domain.Interfaces;
using MiniVentas.Domain.Models.Productos;
using System.Text.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace MiniVentas.Application.Features.Productos.Commands;

public record CreateProductCommand(MantenimientoProductoRequestDto Producto) : IRequest<ApiResponse<int>>;


public record CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ApiResponse<int>>
{

    private readonly IProductoRepository _productoRepository;
    private readonly IMapper _mapper;
    private readonly ILogRepository _logRepository;

    public CreateProductCommandHandler(IProductoRepository productoRepository, IMapper mapper, ILogRepository logRepository)
    {
        _productoRepository = productoRepository;
        _mapper = mapper;
        _logRepository = logRepository;

    }

    public async Task<ApiResponse<int>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var response = new ApiResponse<int>();
        int resultado = 0; 
        string mensajeLog = "";
        string accion = request.Producto.IdOperacion == 1 ? "INSERT" : "UPDATE";

        try
        {
            
            var producto = _mapper.Map<MantenimientoProducto>(request.Producto);

          
            resultado = await _productoRepository.CreateProduct(producto);

           
            if (request.Producto.IdOperacion == 1)
            {
                response.Data = resultado;
                response.Success = resultado > 0;
                response.Message = resultado > 0 ? "Producto registrado correctamente." : "No se pudo registrar el producto.";
            }
            else if (request.Producto.IdOperacion == 2)
            {
                response.Data = resultado;
                response.Success = resultado > 0;
                response.Message = resultado > 0 ? $"Registro modificado correctamente. Filas afectadas: {resultado}" : "No se pudo actualizar el producto.";
            }

            mensajeLog = $"Resultado: {resultado}"; 
        }

        // se comenta por que el middle controla todas las execiones
        //catch (Exception ex)
        //{
        //    response.Data = 0;
        //    response.Success = false;
        //    response.Message = $"Ocurrió un error: {ex.Message}";
        //    mensajeLog = $"Error: {ex.Message}";
        //}
        //se ejecuta siempre
        finally
        {
            
            var detalleJson = JsonSerializer.Serialize(request.Producto);
            var log = new LogTransaccion
            {
                IdUsuario = request.Producto.IdUsuarioRegistro,
                Accion = accion,
                Request = detalleJson,
                Entidad = "PRODUCTOS",       
                Resultado = mensajeLog 
            };

            await _logRepository.InsertarLog(log);
        }

        return response;
    }


}
