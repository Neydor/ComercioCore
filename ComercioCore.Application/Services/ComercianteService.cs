using Application.Common.Pagination;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ComercioCore.Application.DTOs.Comerciante;
using ComercioCore.Application.DTOs.Comerciante.Pagination;
using ComercioCore.Application.DTOs.Reportes;
using ComercioCore.Application.Interfaces.Services;
using ComercioCore.Domain.Entities;
using ComercioCore.Domain.Interfaces.Repositories;
using ComercioCore.Infrastructure.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace ComercioCore.Application.Services
{
    public class ComercianteService : IComercianteService
    {
        private readonly IComercianteRepository _repository;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IMapper _mapper;

        public ComercianteService(
            IComercianteRepository repository, 
            IHttpContextAccessor httpContext,
            IMapper mapper
            )
        {
            _repository = repository;
            _httpContext = httpContext;
            _mapper = mapper;
        }

        public async Task<PagedResponse<ComercianteDto>> GetPagedAsync(ComercianteFilter filter)
        {
            var query = _repository.GetAll()
                .Include(c => c.Municipio)
                .AsQueryable();

            if (!string.IsNullOrEmpty(filter.Nombre))
                query = query.Where(c => c.RazonSocial.Contains(filter.Nombre));

            if (filter.FechaRegistro.HasValue)
                query = query.Where(c => c.FechaRegistro.Date == filter.FechaRegistro.Value.Date);

            if (!string.IsNullOrEmpty(filter.Estado))
                query = query.Where(c => c.Estado == filter.Estado);

            var total = await query.CountAsync();
            var items = await query
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ProjectTo<ComercianteDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return new PagedResponse<ComercianteDto>(items, total, filter.PageNumber, filter.PageSize);
        }

        public async Task<Comerciante> CreateAsync(Comerciante entity)
        {
            SetAuditFields(entity);
            entity.FechaRegistro = DateTime.UtcNow;
            await _repository.AddAsync(entity);
            await _repository.SaveChangesAsync();
            return entity;
        }

        public async Task<Comerciante> UpdateAsync(int id, Comerciante entity)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
            {
                throw new KeyNotFoundException($"Comerciante con ID {id} no encontrado");
            }
            _mapper.Map(entity, existing);
            SetAuditFields(existing);
            _repository.Update(existing);
            await _repository.SaveChangesAsync();
            return existing;
        }

        public async Task UpdateEstadoAsync(int id, string estado)
        {
            var entity = await _repository.GetByIdAsync(id);
            entity.Estado = estado;
            SetAuditFields(entity);
            await _repository.SaveChangesAsync();
        }
        public async Task<Comerciante> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        private void SetAuditFields(Comerciante entity)
        {
            entity.FechaActualizacion = DateTime.UtcNow;
            entity.UsuarioActualizacion = _httpContext.HttpContext?.User?.Identity?.Name ?? "Sistema";
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
            await _repository.SaveChangesAsync();
        }
        public async Task<IEnumerable<ReportesComerciantesActivosDto>> ObtenerComerciantesActivosConEstadisticas()
        {
            var query = _repository.GetAll()
                .Include(c => c.Establecimientos)
                .Where(c => c.Estado == "Activo")
                .Select(c => new ReportesComerciantesActivosDto
                {
                    NombreRazonSocial = c.RazonSocial,
                    Municipio = c.Municipio.Nombre,
                    Telefono = c.Telefono,
                    CorreoElectronico = c.CorreoElectronico,
                    FechaRegistro = c.FechaRegistro,
                    Estado = c.Estado,
                    CantidadEstablecimientos = c.Establecimientos.Count,
                    TotalIngresos = c.Establecimientos.Sum(e => e.Ingresos),
                    TotalEmpleados = c.Establecimientos.Sum(e => e.NumeroEmpleados)
                })
                .OrderByDescending(c=> c.CantidadEstablecimientos);

            return await query.ToListAsync();
        }
    }
}
