using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace CargoPay.Domain.Entities
{
    [Table("Cards")]
    [Index(nameof(CardNumber), IsUnique = true)]
    public class Card
    {
        public Guid CardId { get; set; }
        public required string UserId { get; set; }
        public User? User { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(15)")]
        [StringLength(15, MinimumLength = 15, ErrorMessage = "El número de tarjeta debe tener 15 dígitos.")]
        [RegularExpression("^[0-9]{15}$", ErrorMessage = "El número de tarjeta debe contener solo dígitos.")]
        public required string CardNumber { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Balance { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime UpdatedAt { get; set; }
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
    }
}
