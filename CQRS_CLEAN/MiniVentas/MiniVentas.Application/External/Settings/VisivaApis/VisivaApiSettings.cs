using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniVentas.Application.External.Settings.VisivaApis
{
    public class VisivaApiSettings
    {
        public string AuthUrl { get; set; }
        public string Usuario { get; set; }
        public string Password { get; set; }

        public string ApiKey { get; set; }

        public string CasosEndpoint { get; set; }


    }
}
