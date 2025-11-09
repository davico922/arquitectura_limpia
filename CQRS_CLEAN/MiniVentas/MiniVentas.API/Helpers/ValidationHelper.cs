using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MiniVentas.API.Helpers;

public class ValidationHelper
{
    public static void ValidateModel(ModelStateDictionary modelState)
    {
        if (!modelState.IsValid)
        {
            var errors = string.Join("; ", modelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage));

            throw new ArgumentException($"Los datos de entrada son inválidos: {errors}");
        }
    }

}
