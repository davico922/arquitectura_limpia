using Microsoft.AspNetCore.Razor.Hosting;
using MiniVentas.Domain.Entities.Util;
using NLog;

namespace MiniVentas.API.Middleware;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    //se usa cuando se maneja el nlogger en el arhivo del api
    private static readonly Logger logger=LogManager.GetCurrentClassLogger();

    public ErrorHandlingMiddleware(RequestDelegate next)
    {
            _next=next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            // Validación manual de ModelState si se desactiva el comportamiento automático
            if (!context.Request.HasJsonContentType())
            {
                context.Request.EnableBuffering();
            }

            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        int statusCode;
        string message;

        switch (ex)
        {
            case ArgumentException: // errores de validación
                statusCode = 400;
                message = $"Solicitud inválidaa: {ex.Message}";
                break;
            case UnauthorizedAccessException: // token faltante o inválido
                statusCode = 401;
                message = "No autorizado. Token inválido o faltante.";
                break;
            case InvalidOperationException: // acceso denegado
                statusCode = 403;
                message = "Acceso denegado. No tiene permisos para este recurso.";
                break;
            case KeyNotFoundException: // recurso no encontrado
                statusCode = 404;
                message = $"Recurso no encontrado: {ex.Message}";
                break;
            default: // error general
                statusCode = 500;
                message = $"Ocurrió un error inesperado en la API: {ex.Message}";
                break;
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;

        var response = new ApiResponse<string>
        {
            Success = false,
            Message = message,
            Data = null
        };

        logger.Error(ex, $"Error HTTP {statusCode} capturado por middleware global");

        return context.Response.WriteAsJsonAsync(response);
    }
}

// Extension method para verificar JSON
public static class HttpRequestExtensions
{
    public static bool HasJsonContentType(this HttpRequest request)
    {
        return request.ContentType != null &&
               request.ContentType.Contains("application/json", StringComparison.InvariantCultureIgnoreCase);
    }
}



