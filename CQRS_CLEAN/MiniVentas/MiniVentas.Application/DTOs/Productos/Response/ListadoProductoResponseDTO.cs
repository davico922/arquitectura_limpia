using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniVentas.Application.DTOs.Productos.Response;

public class ListadoProductoResponseDTO
{
    public int Id_Producto { get; set; }

    public string? Nombre { get; set; }

    public int Cantidad { get; set; }

    public decimal Precio { get; set; }

    public string? Estado { get; set; }

    public string? Usuario { get; set; }

}
