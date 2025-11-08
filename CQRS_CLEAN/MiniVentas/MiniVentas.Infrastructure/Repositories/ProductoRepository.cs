using Azure.Core;
using Dapper;
using MiniVentas.Application.DTOs.Productos.Response;
using MiniVentas.Domain.Interfaces;
using MiniVentas.Domain.Models.Productos;
using MiniVentas.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniVentas.Infrastructure.Repositories;

public class ProductoRepository : IProductoRepository
{
    private readonly DapperContext _context;

    public ProductoRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<int> CreateProduct(MantenimientoProducto request)
    {
        var nombreSP = "SP_MANTENIMIENTO_PRODUCTOS";

        var parameters = new DynamicParameters();
        parameters.Add("@ID_OPERACION", request.IdOperacion, DbType.Int32);
        parameters.Add("@ID_PRODUCTO", request.IdProducto, DbType.Int32);
        parameters.Add("@NOMBRE", request.Nombre, DbType.String);
        parameters.Add("@CANTIDAD", request.Cantidad, DbType.Int32);
        parameters.Add("@PRECIO", request.Precio, DbType.Decimal);
        parameters.Add("@ID_ESTADO", request.IdEstado, DbType.Int32);
        parameters.Add("@ID_USUARIO_REGISTRO", request.IdUsuarioRegistro, DbType.Int32);

        using (var con = _context.CreateConnection())
        {
            var resultado = await con.ExecuteScalarAsync<int>(
                nombreSP,
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return resultado; // Este será el ID insertado o 1/0 según la operación
        }
    }
    

    public async Task<List<ListadoProducto>> GetAllProducts(int idProducto)
    {
        var nombreSP = "SP_LISTAR_PRODUCTOS";
       
        var parameters = new DynamicParameters();
        parameters.Add("@ID_PRODUCTO", idProducto, DbType.Int32);

        using (var con = _context.CreateConnection())
        {
            var list = await con.QueryAsync<ListadoProducto>(
                nombreSP,
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return  list.ToList();
        }
    }
}
