using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniVentas.Infrastructure.Context
{
    public class DapperContext
    {

        private readonly string _connectionString;

        public DapperContext(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConexionSQl");
        }

        public IDbConnection CreateConnection()=>new SqlConnection(_connectionString);

    }
}
