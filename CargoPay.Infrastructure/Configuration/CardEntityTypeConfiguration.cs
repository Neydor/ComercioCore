using CargoPay.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CargoPay.Infrastructure.Configuration
{
    public class CardEntityTypeConfiguration : IEntityTypeConfiguration<Card>
    {
        public void Configure(EntityTypeBuilder<Card> builder)
        {
            builder.ToTable("Cards");

            builder.HasKey(c => c.CardId);

            builder.HasOne(c => c.User).
                WithMany(u => u.Cards).
                HasForeignKey(c => c.UserId);

            builder.Property(c => c.CardId)
                .IsRequired()
                .HasColumnType("uniqueidentifier");

            builder.Property(c => c.CardNumber)
                .IsRequired()
                .HasMaxLength(15)
                .HasColumnType("nvarchar(15)");

            builder.HasIndex(c => c.CardNumber)
                .IsUnique();

            builder.Property(c => c.Balance)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(c => c.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

            builder.Property(c => c.UpdatedAt)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

            builder.HasMany(c => c.Payments)
                .WithOne(p => p.Card)
                .HasForeignKey(p => p.CardId);
        }
    }
}
