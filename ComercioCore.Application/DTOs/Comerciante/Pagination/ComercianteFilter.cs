using Application.Common.Pagination;
using System.ComponentModel.DataAnnotations;

namespace ComercioCore.Application.DTOs.Comerciante.Pagination
{
    public class ComercianteFilter : PaginationFilter
    {
        [StringLength(100, ErrorMessage = "Máximo 100 caracteres")]
        public string? Nombre { get; set; }
        public DateTime? FechaRegistro { get; set; }
        [RegularExpression("^(Activo|Inactivo)$",
            ErrorMessage = "Estado debe ser 'Activo' o 'Inactivo'")]
        public string? Estado { get; set; }
    }
}
