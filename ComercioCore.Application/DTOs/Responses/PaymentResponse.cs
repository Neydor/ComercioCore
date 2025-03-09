namespace ComercioCore.Application.DTOs.Responses;

public class PaymentResponse
{
    public int TransactionId { get; set; }
    public decimal Amount { get; set; }
    public decimal Fee { get; set; }
    public decimal TotalAmount => Amount + Fee;
    public decimal NewBalance { get; set; }
    public DateTime Timestamp { get; set; }
    public string Status { get; set; } = string.Empty;  // "Success", "Failed", etc.
}
