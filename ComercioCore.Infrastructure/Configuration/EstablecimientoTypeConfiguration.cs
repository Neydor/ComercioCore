using ComercioCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ComercioCore.Infrastructure.Configuration
{
    public class EstablecimientoTypeConfiguration : IEntityTypeConfiguration<Establecimiento>
    {
        public void Configure(EntityTypeBuilder<Establecimiento> builder)
        {
            builder.HasKey(e => e.Id).HasName("PK__Establec__3214EC27AF100268");

            builder.ToTable("Establecimiento", tb => tb.HasTrigger("TRG_Establecimiento_Auditoria"));

            builder.Property(e => e.Id).HasColumnName("ID");
            builder.Property(e => e.ComercianteId).HasColumnName("ComercianteID");
            builder.Property(e => e.FechaActualizacion).HasColumnType("datetime");
            builder.Property(e => e.Ingresos).HasColumnType("decimal(18, 2)");
            builder.Property(e => e.NombreEstablecimiento)
                .HasMaxLength(100)
                .IsUnicode(false);
            builder.Property(e => e.UsuarioActualizacion)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.HasOne(d => d.Comerciante).WithMany(p => p.Establecimientos)
                .HasForeignKey(d => d.ComercianteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Estableci__Comer__3F466844");
        }
    }
}
