using Azure.Core;
using Dapper;
using MiniVentas.Domain.Entities.Logs;
using MiniVentas.Domain.Interfaces;
using MiniVentas.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniVentas.Infrastructure.Repositories;

public class LogRepository : ILogRepository
{

    private readonly DapperContext _dapperContext;

    public LogRepository(DapperContext context)
    {
        _dapperContext = context;
            
    }

    public async Task InsertarLog(LogTransaccion log)
    {

        var nombreSP = "SP_InsertarLogTransaccion";

        var parameters = new DynamicParameters();
        parameters.Add("@IdUsuario", log.IdUsuario, DbType.Int32);
        parameters.Add("@Accion", log.Accion, DbType.String);
        parameters.Add("@Entidad", log.Entidad, DbType.String);
        parameters.Add("@Request", log.Request, DbType.String);
        parameters.Add("@Resultado", log.Resultado, DbType.String);

        using (var con = _dapperContext.CreateConnection())
        {
            var resultado = await con.ExecuteAsync(
                nombreSP,
                parameters,
                commandType: CommandType.StoredProcedure
            );

         
        }
    }
}
