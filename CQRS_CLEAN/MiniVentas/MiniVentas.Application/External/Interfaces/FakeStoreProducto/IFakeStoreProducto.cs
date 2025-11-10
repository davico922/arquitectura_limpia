using MiniVentas.Application.External.DTOs.FakeStoreProducto.CreateProducto.Request;
using MiniVentas.Application.External.DTOs.FakeStoreProducto.CreateProducto.Response;
using MiniVentas.Application.External.DTOs.FakeStoreProducto.GetProductoById.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniVentas.Application.External.Interfaces.FakeStoreProducto
{
    public interface IFakeStoreProducto
    {

        Task<FakeStoreProductByIdResponseDto> GetProductByIdAsync(int id);

        Task<FakeStoreCreateProductoResponseDto> CreateProductoAsync(FakeStoreCreateProductoRequestDto request);
    }
}
