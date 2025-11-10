using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MiniVentas.Application.External.Interfaces.FakeStoreProducto;
using MiniVentas.Application.External.Settings.FakeStoreProducto;
using MiniVentas.Domain.Interfaces;
using MiniVentas.Infrastructure.Context;
using MiniVentas.Infrastructure.ExternalServices.FakeStoreProducto;
using MiniVentas.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MiniVentas.Infrastructure;

public static  class DependencyInjection
{

    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
    {
        //registra el dapper ,las cadenas de conexion
         services.AddSingleton<DapperContext>();

        //registrmos los repositorios y su interfaz

        services.AddScoped<IProductoRepository,ProductoRepository>();

        services.AddScoped<ILogRepository,LogRepository>();


        // apis

        //para los apis 

        //registro los keys

        services.Configure<FakeStoreApiSettings>(
                 configuration.GetSection("FakeStoreApiSettings")
             );

        //registros la interface
        services.AddHttpClient<IFakeStoreProducto, FakeStoreProductoRepositoryProxy>();



        return services;
    }


}
