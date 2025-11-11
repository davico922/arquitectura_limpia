using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniVentas.Application.DTOs.Casos.Response
{
    public class ListadoSolicitudesFilterResponseDto
    {

      
            public List<Caso> casos { get; set; }
        

        public class Caso
        {
            public string id { get; set; }
            public string nro_Solicitud_Portal__c { get; set; }
            public string caseNumber { get; set; }
            public string type { get; set; }
            public string proceso__c { get; set; }
            public string motivo__c { get; set; }
            public string grado_academico__c { get; set; }
            public string programa_academico__c { get; set; }
            public string periodo_Academico__c { get; set; }
            public string status { get; set; }
            public string description { get; set; }
            public string url_adjunto1__c { get; set; }
            public string url_adjunto2__c { get; set; }
            public string url_adjunto3__c { get; set; }
            public string resultado__c { get; set; }
            public string resultado_Detalle__c { get; set; }
            public string createdDate { get; set; }
            public string numero_de_clase__c { get; set; }
            public string codigo_del_curso__c { get; set; }
            public string nombre_del_curso__c { get; set; }
            public string periodo_destino__c { get; set; }
            public string plan_destino__c { get; set; }
            public string programa_destino__c { get; set; }
            public string sede_destino__c { get; set; }
            public string sedeEntrega { get; set; }
            public string procedenciaSolicitud { get; set; }
            public string idSubConsulta { get; set; }
            public string examenModular { get; set; }
            public string idTipoEvaluacion { get; set; }
            public string cod_Externo { get; set; }
            public string mensajeValidacion { get; set; }
            public string subMensajeValidacion { get; set; }
            public string fechaCitaConsejeria { get; set; }
            public string consejero { get; set; }
            public string motivoCita { get; set; }
            public string horaCitaConsejeria { get; set; }
            public string contenidoID { get; set; }
            public string idReserva { get; set; }
            public string fechaReserva { get; set; }
            public string nombreEspacio { get; set; }
            public string horaInicio_Reserva { get; set; }
            public string horaFin_Reserva { get; set; }
            public string motivo_Reserva { get; set; }
        }
    }
}
