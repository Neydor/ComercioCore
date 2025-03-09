using ComercioCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ComercioCore.Infrastructure.Configuration
{
    public class UsuarioEntityTypeConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasKey(e => e.Id).HasName("PK__Usuario__3214EC276995779B");

            builder.ToTable("Usuario");

            builder.HasIndex(e => e.CorreoElectronico, "UQ__Usuario__531402F38D061136").IsUnique();

            builder.Property(e => e.Id).HasColumnName("ID");
            builder.Property(e => e.Contrasena)
                .HasMaxLength(100)
                .IsUnicode(false);
            builder.Property(e => e.CorreoElectronico)
                .HasMaxLength(100)
                .IsUnicode(false);
            builder.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
            builder.Property(e => e.Rol)
                .HasMaxLength(20)
                .IsUnicode(false);
        }
    }
}
