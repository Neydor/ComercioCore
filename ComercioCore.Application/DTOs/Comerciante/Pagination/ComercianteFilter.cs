using Application.Common.Pagination;

namespace ComercioCore.Application.DTOs.Comerciante.Pagination
{
    public class ComercianteFilter : PaginationFilter
    {
        public string Nombre { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public string Estado { get; set; }
    }
}
