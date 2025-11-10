using Azure;
using Microsoft.Extensions.Options;
using MiniVentas.Application.External.DTOs.FakeStoreProducto.CreateProducto.Request;
using MiniVentas.Application.External.DTOs.FakeStoreProducto.CreateProducto.Response;
using MiniVentas.Application.External.DTOs.FakeStoreProducto.GetProductoById.Response;
using MiniVentas.Application.External.Interfaces.FakeStoreProducto;
using MiniVentas.Application.External.Settings.FakeStoreProducto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MiniVentas.Infrastructure.ExternalServices.FakeStoreProducto
{
    public class FakeStoreProductoRepositoryProxy : IFakeStoreProducto
    {

        private readonly HttpClient _httpClient;
        private readonly FakeStoreApiSettings _settings;

        //IOptions<FakeStoreApiSettings>  es la forma oficial de inyectar configuración del appsettings.json en tus clases dentro de .NET
        public FakeStoreProductoRepositoryProxy(HttpClient httpClient ,IOptions<FakeStoreApiSettings> settings)
        {
              _httpClient= httpClient;
            //te da acceso final a esos valores.
            _settings = settings.Value;
        }

        public async Task<FakeStoreProductByIdResponseDto> GetProductByIdAsync(int id)
        {
          
            var url = $"{ _settings.BaseUrl}{ _settings.GetProductById }{id}" ;

            var response = await _httpClient.GetAsync($"{url}");
            var json = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<FakeStoreProductByIdResponseDto>(json);
            return data;


        }

        public async Task<FakeStoreCreateProductoResponseDto> CreateProductoAsync(FakeStoreCreateProductoRequestDto request)
        {
            var url = $"{_settings.BaseUrl}{_settings.GetProductById}";

            var response = await _httpClient.PostAsJsonAsync($"{url}",request);
            var json = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<FakeStoreCreateProductoResponseDto>(json);

            return data;
        }

       
    }
}
