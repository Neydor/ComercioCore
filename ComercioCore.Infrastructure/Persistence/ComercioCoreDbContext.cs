using ComercioCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ComercioCore.Infrastructure.Persistence;

public partial class ComercioCoreDbContext : DbContext
{
    private readonly IConfiguration _configuration;

    public ComercioCoreDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public ComercioCoreDbContext(DbContextOptions<ComercioCoreDbContext> options, IConfiguration configuration)
        : base(options)
    {
        _configuration = configuration;
    }

    public virtual DbSet<Comerciante> Comerciantes { get; set; }

    public virtual DbSet<Establecimiento> Establecimientos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }
    public virtual DbSet<Municipio> Municipio { get; set; }
    public DbSet<ReporteComercianteActivoSP> Reporte { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString,
                b => b.MigrationsAssembly("ComercioCore.Infrastructure"));
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ComercioCoreDbContext).Assembly);
        modelBuilder.Entity<ReporteComercianteActivoSP>().HasNoKey();
        modelBuilder.Entity<Comerciante>()
       .HasMany(c => c.Establecimientos)
       .WithOne(e => e.Comerciante)
       .OnDelete(DeleteBehavior.Cascade);
    }
    
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
