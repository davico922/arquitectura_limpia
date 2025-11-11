using Microsoft.Extensions.Options;
using MiniVentas.Application.External.DTOs.VisivaApis.Token.Response;
using MiniVentas.Application.External.DTOs.VisivaApis.VisivaCasos.Request;
using MiniVentas.Application.External.DTOs.VisivaApis.VisivaCasos.Response;
using MiniVentas.Application.External.Settings.VisivaApis;
using MiniVentas.Domain.Interfaces.External.VisivaApis;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MiniVentas.Infrastructure.ExternalServices.VisivaApi
{
    public class VisivaApiRepository : IVisivaApiRepository
    {

        private readonly HttpClient _httpClient;
        private readonly VisivaApiSettings _settings;

        public VisivaApiRepository(HttpClient httpClient,IOptions<VisivaApiSettings> settings)
        {
            _httpClient = httpClient;
            _settings = settings.Value;
        }

      
        public async Task<string> ObtenerTokenAsync()
        {

            var body = new
            {

                usuario = _settings.Usuario,
                password = _settings.Password
            };

            var content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");
            _httpClient.DefaultRequestHeaders.Add("x-api-key", _settings.ApiKey);
            var response = await _httpClient.PostAsync(_settings.AuthUrl, content);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<VisivaAuthResponseDto>(json);

            return data?.Detalle?.token ?? string.Empty;
        }

 
        public async Task<VisivaCasosResponseDto> ObtenerCasosAsync(string token, VisivaCasosRequestDto request)
        {
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            _httpClient.DefaultRequestHeaders.Add("x-api-key", _settings.ApiKey);

            var response = await _httpClient.PostAsJsonAsync(_settings.CasosEndpoint, request);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<VisivaCasosResponseDto>(json);
        }
    }
}
