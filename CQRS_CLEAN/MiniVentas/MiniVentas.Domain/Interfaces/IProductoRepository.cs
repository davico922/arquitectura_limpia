using MiniVentas.Domain.Models.Productos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MiniVentas.Domain.Interfaces
{
    public interface IProductoRepository
    {
        Task<int> CreateProduct(MantenimientoProducto request);
        Task<List<ListadoProducto>> GetAllProducts(int idProducto);
    }
}
