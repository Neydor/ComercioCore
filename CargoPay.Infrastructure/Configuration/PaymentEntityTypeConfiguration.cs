using CargoPay.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CargoPay.Infrastructure.Configuration
{
    public class PaymentEntityTypeConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            // Configure table name
            builder.ToTable("Payments");

            // Configure primary key
            builder.HasKey(p => p.PaymentId);

            // Configure properties
            builder.Property(p => p.PaymentId)
                .IsRequired();

            builder.Property(p => p.CardId)
                .IsRequired();

            builder.Property(p => p.Amount)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(p => p.Fee)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(p => p.Timestamp)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

            // Configure relationships
            builder.HasOne(p => p.Card)
                .WithMany(c => c.Payments)
                .HasForeignKey(p => p.CardId);
        }
    }
}
