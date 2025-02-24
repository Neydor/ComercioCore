using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using CargoPay.Domain.Entities;

namespace CargoPay.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public ApplicationDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }

        public DbSet<Card> Cards => Set<Card>();
        public DbSet<Payment> Payments => Set<Payment>();
        public DbSet<FeeHistory> FeesHistory => Set<FeeHistory>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = _configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString, 
                    b => b.MigrationsAssembly("CargoPay.Infrastructure"));
            }
        }
    }
}
