using Application.Common.Pagination;
using ComercioCore.Application.DTOs.Comerciante;
using ComercioCore.Application.DTOs.Comerciante.Pagination;
using ComercioCore.Application.DTOs.Reportes;
using ComercioCore.Domain.Entities;

namespace ComercioCore.Application.Interfaces.Services
{
    public interface IComercianteService
    {
        Task<PagedResponse<ComercianteDto>> GetPagedAsync(ComercianteFilter filter);
        Task<Comerciante> CreateAsync(Comerciante entity);
        Task UpdateEstadoAsync(int id, string estado);
        Task<Comerciante> GetByIdAsync(int id);
        Task<Comerciante> UpdateAsync(int id, ComercianteUpdateDto entity);
        Task DeleteAsync(int id);
        Task<IEnumerable<ReporteComercianteActivoSP>> ObtenerComerciantesActivosConEstadisticas();
    }
}
