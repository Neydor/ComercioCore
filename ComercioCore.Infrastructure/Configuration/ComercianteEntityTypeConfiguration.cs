using ComercioCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ComercioCore.Infrastructure.Configuration
{
    public class ComercianteEntityTypeConfiguration : IEntityTypeConfiguration<Comerciante>
    {
        public void Configure(EntityTypeBuilder<Comerciante> entity)
        {
            entity.HasKey(e => e.Id).HasName("PK__Comercia__3214EC27CD585752");

            entity.ToTable("Comerciante", tb => tb.HasTrigger("TRG_Comerciante_Auditoria"));

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CorreoElectronico)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Estado)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.FechaActualizacion).HasColumnType("datetime");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.RazonSocial)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioActualizacion)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Municipio).WithMany(p => p.Comerciantes)
                .HasForeignKey(d => d.MunicipioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Comercian__Munic__3D5E1FD2");
        }
    }
}
