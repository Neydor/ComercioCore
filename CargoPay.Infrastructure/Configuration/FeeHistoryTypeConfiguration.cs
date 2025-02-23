using CargoPay.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoPay.Infrastructure.Configuration
{
    public class FeeHistoryTypeConfiguration : IEntityTypeConfiguration<FeeHistory>
    {

        public void Configure(EntityTypeBuilder<FeeHistory> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(c => c.EffectiveTo)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

            builder.Property(c => c.EffectiveFrom)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

            builder.Property(c => c.FeeRate)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.ToTable("FeesHistory");
        }
    }
}
