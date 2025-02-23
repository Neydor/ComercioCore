using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CargoPay.Domain.Entities
{
    [Table("Payments")]
    public class Payment
    {
        #region Properties 

        [Key]
        public Guid PaymentId { get; set; }

        [Required]
        public Guid CardId { get; set; }

        [ForeignKey("CardId")]
        public Card Card { get; set; } = null!;

        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Fee { get; set; }

        public DateTime Timestamp { get; set; }


        #endregion
    }
}
