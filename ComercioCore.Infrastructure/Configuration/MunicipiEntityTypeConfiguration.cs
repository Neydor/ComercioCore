using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComercioCore.Domain.Entities;
using System.Reflection.Emit;

namespace ComercioCore.Infrastructure.Configuration
{
    public class MunicipiEntityTypeConfiguration : IEntityTypeConfiguration<Municipio>
    {
        public void Configure(EntityTypeBuilder<Municipio> builder)
        {

            builder.HasKey(e => e.Id).HasName("PK__Municipi__3214EC2702B8AD48");

            builder.ToTable("Municipio");

            builder.Property(e => e.Id).HasColumnName("ID");
            builder.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
        }
    }
}
