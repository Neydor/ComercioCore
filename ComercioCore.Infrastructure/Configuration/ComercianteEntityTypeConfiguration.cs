using ComercioCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ComercioCore.Infrastructure.Configuration
{
    public class ComercianteEntityTypeConfiguration : IEntityTypeConfiguration<Comerciante>
    {
        public void Configure(EntityTypeBuilder<Comerciante> builder)
        {
            builder.HasKey(e => e.Id).HasName("PK__Comercia__3214EC27F410B0B9");

            builder.ToTable("Comerciante", tb => tb.HasTrigger("TRG_Comerciante_Auditoria"));

            builder.Property(e => e.Id).HasColumnName("ID");
            builder.Property(e => e.CorreoElectronico)
                .HasMaxLength(100)
                .IsUnicode(false);
            builder.Property(e => e.Estado)
                .HasMaxLength(10)
                .IsUnicode(false);
            builder.Property(e => e.FechaActualizacion).HasColumnType("datetime");
            builder.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            builder.HasOne(c => c.Municipio)
                .WithMany()
                .HasForeignKey(c => c.MunicipioId);
            builder.Property(e => e.RazonSocial)
                .HasMaxLength(100)
                .IsUnicode(false);
            builder.Property(e => e.Telefono)
                .HasMaxLength(20)
                .IsUnicode(false);
            builder.Property(e => e.UsuarioActualizacion)
                .HasMaxLength(100)
                .IsUnicode(false);
        }
    }
}
