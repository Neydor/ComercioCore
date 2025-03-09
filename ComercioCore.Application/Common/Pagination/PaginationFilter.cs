using System.ComponentModel.DataAnnotations;

namespace Application.Common.Pagination
{
    public abstract class PaginationFilter
    {
        [Range(1, int.MaxValue, ErrorMessage = "El número de página debe ser al menos 1")]
        public int PageNumber { get; set; } = 1;

        [Range(1, 100, ErrorMessage = "El tamaño de página debe estar entre 1 y 100")]
        public int PageSize { get; set; } = 5;
    }
}