using AutoMapper;
using MediatR;
using MiniVentas.Application.DTOs.Productos.Response;
using MiniVentas.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MiniVentas.Application.Features.Productos.Queries;



public record GetAllProductsQuery (int idProducto) : IRequest<List<ListadoProductoResponseDTO>>;


public class GetallProductQueryHandler : IRequestHandler<GetAllProductsQuery, List<ListadoProductoResponseDTO>>
{
    
        private readonly IProductoRepository _productoRepository;
    private readonly IMapper _mapper;

    public GetallProductQueryHandler(IProductoRepository productoRepository,IMapper mapper)

    {
        _productoRepository = productoRepository;
        _mapper= mapper;
    }

    public async Task<List<ListadoProductoResponseDTO>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var productos = await _productoRepository.GetAllProducts(request.idProducto);

        // sin automapper
        //var result = productos.Select(p => new ListadoProductoResponseDTO
        //{
        //    Id_Producto = p.Id_Producto,
        //    Nombre = p.Nombre,
        //    Cantidad = p.Cantidad,
        //    Precio = p.Precio,
        //    Estado = p.Estado,
        //    Usuario = p.Usuario
        //}).ToList();

        //return result;

        //con automapper

        return _mapper.Map<List<ListadoProductoResponseDTO>>(productos);

    }
}


