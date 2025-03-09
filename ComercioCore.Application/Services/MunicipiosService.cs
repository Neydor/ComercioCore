// Application/Services/MunicipioService.cs
using ComercioCore.Application.Interfaces.Services;
using ComercioCore.Domain.Entities;
using ComercioCore.Domain.Interfaces.Repositories;
using ComercioCore.Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.Caching.Memory;

public class MunicipioService : IMunicipioService
{
    private readonly IMunicipioRepository _municipioRepository;
    private readonly IMemoryCache _cache;
    private const string CACHE_KEY = "MunicipiosList";
    private readonly TimeSpan CACHE_EXPIRATION = TimeSpan.FromHours(1);

    public MunicipioService(
        IMunicipioRepository municipioRepository,
        IMemoryCache cache)
    {
        _municipioRepository = municipioRepository;
        _cache = cache;
    }

    public async Task<IEnumerable<Municipio>?> ObtenerTodos()
    {
        return await _cache.GetOrCreateAsync<IEnumerable<Municipio>?>(CACHE_KEY, async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = CACHE_EXPIRATION;

            var comerciantes = await _municipioRepository.ObtenerTodos();
            return comerciantes;
        });
    }
}