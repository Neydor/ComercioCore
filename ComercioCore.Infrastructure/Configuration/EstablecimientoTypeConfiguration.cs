using ComercioCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ComercioCore.Infrastructure.Configuration
{
    public class EstablecimientoTypeConfiguration : IEntityTypeConfiguration<Establecimiento>
    {
        public void Configure(EntityTypeBuilder<Establecimiento> entity)
        {
            entity.HasKey(e => e.Id).HasName("PK__Establec__3214EC27A742C58D");

            entity.ToTable("Establecimiento", tb => tb.HasTrigger("TRG_Establecimiento_Auditoria"));

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ComercianteId).HasColumnName("ComercianteID");
            entity.Property(e => e.FechaActualizacion).HasColumnType("datetime");
            entity.Property(e => e.Ingresos).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.NombreEstablecimiento)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioActualizacion)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Comerciante).WithMany(p => p.Establecimientos)
                .HasForeignKey(d => d.ComercianteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Estableci__Comer__4222D4EF");
        }
    }
}
