using AutoMapper;
using MiniVentas.Application.DTOs.Productos.Response;
using MiniVentas.Domain.Models.Productos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniVentas.Application.Mappings.Productos;

public class ProductoProfile :Profile
{
    public ProductoProfile()
    {
        // 🔹 Mapeo directo (mismos nombres)
        CreateMap<ListadoProducto, ListadoProductoResponseDTO>();

        // 🔹 Ejemplo de mapeo con nombres distintos (si lo tuvieras)
        // CreateMap<ListadoProducto, ListadoProductoResponseDTO>()
        //     .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.NombreProducto))
        //     .ForMember(dest => dest.Usuario, opt => opt.MapFrom(src => src.UsuarioRegistro));  

        //registrar

       // CreateMap<RegistrarProductoRequestDTO, Producto>().ReverseMap();
    }

}
